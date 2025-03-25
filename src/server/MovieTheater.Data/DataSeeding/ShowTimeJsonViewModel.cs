namespace MovieTheater.Data.DataSeeding;

public class ShowTimeJsonViewModel
{
    public required Guid ShowTimeId { get; set; }
    public required Guid MovieId { get; set; }
    public required Guid CinemaRoomId { get; set; }
    public required Guid ShowTimeSlotId { get; set; }
    public required string ShowDate { get; set; }
}
