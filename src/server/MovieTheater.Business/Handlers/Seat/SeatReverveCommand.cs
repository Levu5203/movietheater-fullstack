using System;
using MediatR;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Business.ViewModels.Ticket;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatReverveCommand : IRequest<IEnumerable<TicketViewModel>>
{
    public Guid ShowTimeId { get; set; }
    public List<Guid>? SeatIds { get; set; }
}
