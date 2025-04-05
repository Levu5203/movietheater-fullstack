using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Business.ViewModels.Promotion;
using MovieTheater.Models.Common;
using MovieTheater.Business.ViewModels.Room;

namespace MovieTheater.Business.ViewModels.Ticket;

public class TicketViewModel : MasterBaseViewModel
{
    public decimal Price { get; set; }
    public DateTime BookingDate { get; set; }
    public required TicketStatus Status { get; set; }
    public SeatViewModel? Seat { get; set; }
    public required string RoomName { get; set; }
    public required string MovieName { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid? PromotionId { get; set; }
    public PromotionViewModel? Promotion { get; set; }
    public required DateOnly ShowDate { get; set; }
    public required TimeSpan StartTime { get; set; }
}
