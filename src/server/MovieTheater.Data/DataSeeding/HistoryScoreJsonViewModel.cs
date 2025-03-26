namespace MovieTheater.Data.DataSeeding;

public class HistoryScoreJsonViewModel
{
    public required Guid Id { get; set; }
    public required int Score { get; set; }
    public required int ScoreStatus { get; set; }
    public required string Description { get; set; }
    public required Guid InvoiceId { get; set; }
    public required DateTime CreatedAt { get; set; }
}