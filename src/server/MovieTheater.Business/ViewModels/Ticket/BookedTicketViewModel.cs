using System;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Ticket;

public class BookedTicketViewModel
{
    public required string MovieName { get; set; }
    public required string CinemaRoomName { get; set; }
    public required DateTime ShowDate { get; set; }
    public required TimeSpan ShowTime { get; set; }
    public required string SeatPosition { get; set; }
    public required DateTime BookingDate { get; set; }
    public required decimal Price { get; set; }
    public required TicketStatus Status { get; set; }
}
