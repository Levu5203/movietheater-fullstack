using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Ticket;
using MovieTheater.Data;


namespace MovieTheater.Business.Handlers.Ticket;


public class GetBookedTicketsByMemberIdHandler : IRequestHandler<GetBookedTicketsByMemberIdQuery, List<BookedTicketViewModel>>
{
    private readonly MovieTheaterDbContext _context;


    public GetBookedTicketsByMemberIdHandler(MovieTheaterDbContext context)
    {
        _context = context;
    }


    public async Task<List<BookedTicketViewModel>> Handle(GetBookedTicketsByMemberIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tickets
            .Where(t => t.Invoice.UserId == request.MemberId)
            .Select(t => new BookedTicketViewModel
            {
                MovieName = t.Invoice.ShowTime.Movie.Name,
                BookingDate = t.BookingDate,
                TotalMoney = t.Invoice.TotalMoney,
                Status = t.Status
            })
            .ToListAsync(cancellationToken);
    }
}
