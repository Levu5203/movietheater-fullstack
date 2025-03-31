using System;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Ticket;

public class TicketViewModel
{
    public Guid TicketId { get; set; }
    public double Price { get; set; }
    public DateTime BookingDate { get; set; }
    public TicketStatus Status { get; set; }
    public Guid SeatId { get; set; }
    public SeatViewModel? Seat { get; set; }
}
