using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Customers;

public class CustomerUpdateStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IUserIdentity currentUser) :
    BaseHandler(unitOfWork, mapper),
    IRequestHandler<CustomerUpdateStatusCommand, UserViewModel>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IUserIdentity _currentUser = currentUser;

    public async Task<UserViewModel> Handle(CustomerUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var currUser = await _unitOfWork.UserRepository.GetByIdAsync(_currentUser.UserId) ??
           throw new UnauthorizedAccessException($"User with ID {_currentUser.UserId} not found");

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with {request.Id} is not found");
        }

        user.IsActive = !user.IsActive;
        user.UpdatedById = currUser.Id;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to update customer: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return _mapper.Map<UserViewModel>(user);
    }
}