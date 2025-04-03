using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Ticket;
using MovieTheater.Data;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;


namespace MovieTheater.Business.Handlers.Ticket;


public class GetBookedTicketsByMemberIdHandler : IRequestHandler<GetBookedTicketsByMemberIdQuery, List<BookedTicketViewModel>>
{
    private readonly MovieTheaterDbContext _context;

    private readonly IUnitOfWork _unitOfWork;

    public GetBookedTicketsByMemberIdHandler(MovieTheaterDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<BookedTicketViewModel>> Handle(GetBookedTicketsByMemberIdQuery request, CancellationToken cancellationToken)
    {
        var ticketsQuery = _unitOfWork.TicketRepository
            .GetQuery(t => t.Invoice.UserId == request.MemberId)
            .Include(t => t.Invoice.ShowTime.Movie);

        var tickets = await ticketsQuery.ToListAsync(cancellationToken);

        return tickets.Select(t => new BookedTicketViewModel
        {
            MovieName = t.Invoice.ShowTime.Movie.Name,
            BookingDate = t.BookingDate,
            TotalMoney = t.Invoice.TotalMoney,
            Status = t.Status 
        }).ToList();
    }
}
