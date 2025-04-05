using MovieTheater.Models.Common;

namespace MovieTheater.Data.DataSeeding;

public class TicketJsonViewModel
{
    public required Guid Id { get; set; }
    public required char Row { get; set; }
    public required int Column { get; set; }

    public required decimal Price { get; set; }

    public required DateTime BookingDate { get; set; }

    public required Guid SeatId { get; set; }

    public Seat? Seat { get; set; }

    public required Guid CinemaRoomId { get; set; }

    public CinemaRoom? CinemaRoom { get; set; }

    public required Guid ShowTimeId { get; set; }

    public ShowTime? ShowTime { get; set; }

    public required Guid MovieId { get; set; }

    public Movie? Movie { get; set; }

    public required Guid InvoiceId { get; set; }

    public Invoice? Invoice { get; set; }

    public required TicketStatus Status { get; set; }

    public Guid? PromotionId { get; set; }

    public Promotion? Promotion { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}
