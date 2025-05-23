using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Core;
using MovieTheater.Core.Extensions;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Customers;

public class CustomerSearchQueryHandler :
    BaseHandler,
    IRequestHandler<CustomerSearchQuery, PaginatedResult<UserViewModel>>
{
    private readonly UserManager<User> _userManager;
    public CustomerSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
    {
        _userManager = userManager;
    }

    public async Task<PaginatedResult<UserViewModel>> Handle(
        CustomerSearchQuery request,
        CancellationToken cancellationToken)
    {
        // Create query
        var customerList = await _userManager.GetUsersInRoleAsync("Customer");
        var userIdList = customerList.Select(u => u.Id).ToList();

        var query = _unitOfWork.UserRepository.GetQuery(request.IncludeInactive!.Value)
            .Where(u => userIdList.Contains(u.Id));

        // Filter by inactive status
        if (request.IncludeInactive.HasValue && !request.IncludeInactive.Value)
        {
            query = query.Where(u => u.IsActive);
        }
        // Filter by keyword
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            var keywordLower = request.Keyword.ToLower();

            query = query.Where(x =>
                x.FirstName.ToLower().Contains(keywordLower) ||
                x.LastName.ToLower().Contains(keywordLower) ||
                x.UserName!.ToLower().Contains(keywordLower) ||
                x.Email!.ToLower().Contains(keywordLower) ||
                x.PhoneNumber!.Contains(keywordLower)
            );
        }

        // Filter by gender
        if (!string.IsNullOrEmpty(request.Gender))
        {
            query = query.Where(x => x.Gender == request.Gender);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(u => u.IsActive == request.IsActive.Value);
        }

        // Count total items
        int total = await query.CountAsync(cancellationToken);

        // Sort
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = query.OrderByExtension(request.OrderBy, request.OrderDirection.ToString());
        }
        else
        {
            query = query.OrderBy(x => x.UserName);
        }

        // Get data with pagination
        var items = await query.Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .Include(x => x.CreatedBy)
            .Include(x => x.UpdatedBy)
            .Include(x => x.DeletedBy)
            .ToListAsync(cancellationToken);

        // Map to view models
        var viewModels = _mapper.Map<IEnumerable<UserViewModel>>(items);

        // Return paginated result
        return new PaginatedResult<UserViewModel>(request.PageNumber, request.PageSize, total, viewModels);
    }
}
