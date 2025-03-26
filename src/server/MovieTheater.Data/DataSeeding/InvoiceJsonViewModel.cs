namespace MovieTheater.Data.DataSeeding;

public class InvoiceJsonViewModel
{
    public required Guid Id { get; set; }
    public required double TotalMoney { get; set; }
    public required int AddedScore { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ShowTimeId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required Guid CreatedById { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}
