using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Core;
using MovieTheater.Core.Extensions;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Invoice;

public class InvoiceSearchQueryHandler : BaseHandler, IRequestHandler<InvoiceSearchQuery, PaginatedResult<InvoiceViewModel>>
{
    public InvoiceSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<PaginatedResult<InvoiceViewModel>> Handle(InvoiceSearchQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.InvoiceRepository.GetQuery().Where(i => i.InvoiceStatus == InvoiceStatus.Paid);

        // Filter by keyword
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            var keywordLower = request.Keyword.ToLower();

            query = query.Where(x =>
                x.User.FirstName.ToLower().Contains(keywordLower) || x.User.LastName.ToLower().Contains(keywordLower) || x.User.Email.ToLower().Contains(keywordLower) || x.User.PhoneNumber.ToLower().Contains(keywordLower) || x.Movie.Name.ToLower().Contains(keywordLower)
            );
        }

        // Filter by TicketIssued
        if (request.TicketIssued.HasValue)
        {
            query = query.Where(u => u.TicketIssued == request.TicketIssued);
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
            query = query.OrderBy(x => x.ShowTime.ShowDate);
        }

        // Include related entities (only navigation properties)
        query = query
            .Include(i => i.User)
            .Include(i => i.ShowTime)
                .ThenInclude(s => s!.ShowTimeSlot)
            .Include(i => i.CinemaRoom)
            .Include(i => i.Movie)
            .Include(i => i.Tickets)
                .ThenInclude(t => t.Seat);

        // Get data with pagination
        var items = await query.Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // Map to view models
        var viewModels = _mapper.Map<IEnumerable<InvoiceViewModel>>(items);

        // Return paginated result
        return new PaginatedResult<InvoiceViewModel>(request.PageNumber, request.PageSize, total, viewModels);
    }
}
