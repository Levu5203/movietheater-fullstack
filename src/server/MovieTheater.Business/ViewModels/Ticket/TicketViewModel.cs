using System;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Ticket;

public class TicketViewModel
{
    public Guid TicketId { get; set; }
    public decimal Price { get; set; }
    public DateTime BookingDate { get; set; }
    public TicketStatus Status { get; set; }
    public Guid SeatId { get; set; }
    public SeatViewModel? Seat { get; set; }
    public Guid CinemaRoomId { get; set; }
    public Guid ShowTimeId { get; set; }
    public ShowtimeViewModel? ShowTime { get; set; }
    public Guid MovieId { get; set; }
    public MovieViewModel? Movie { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid? PromotionId { get; set; }
}
