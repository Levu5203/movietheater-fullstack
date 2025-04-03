using MovieTheater.Models.Common;
using MovieTheater.Models.Security;

namespace MovieTheater.Data.DataSeeding;

public class InvoiceJsonViewModel
{
    public required Guid Id { get; set; }

    public required decimal TotalMoney { get; set; }

    public required int AddedScore { get; set; }

    public bool TicketIssued { get; set; } = false;

    public required Guid UserId { get; set; }

    public User? User { get; set; }

    public required Guid ShowTimeId { get; set; }

    public ShowTime? ShowTime { get; set; }

    public required Guid CinemaRoomId { get; set; }

    public CinemaRoom? CinemaRoom { get; set; }

    public required Guid MovieId { get; set; }

    public Movie? Movie { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required Guid CreatedById { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}
