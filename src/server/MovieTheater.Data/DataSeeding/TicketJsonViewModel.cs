namespace MovieTheater.Data.DataSeeding;

public class TicketJsonViewModel
{
    public required Guid Id { get; set; }
    public required double Price { get; set; }
    public required DateTime BookingDate { get; set; }
    public required string Status { get; set; }
    public required Guid InvoiceId { get; set; }
    public required Guid CinemaRoomId { get; set; }
    public required char Row { get; set; }
    public required int Column { get; set; }
    public required Guid PromotionId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}
