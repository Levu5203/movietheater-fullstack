using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Ticket;

public class HistoryScoreViewModel
{
    public required string MovieName { get; set; }
    public required DateTime BookingDate { get; set; }
    public required int AddedScore { get; set; }
    public required ScoreStatus Status { get; set; }
}
