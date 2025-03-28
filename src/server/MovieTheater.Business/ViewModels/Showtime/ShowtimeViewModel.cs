namespace MovieTheater.Business.ViewModels.Showtime;

public class ShowtimeViewModel : MasterBaseViewModel
{
    public Guid MovieId {get; set;}
    public Guid RoomId {get; set;}
    public Guid ShowtimeSlotId {get; set;}
    public DateOnly ShowDate {get; set;}
    public string? MovieName {get; set;}
    public string? RoomName {get; set;}
}
