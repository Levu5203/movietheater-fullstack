namespace MovieTheater.Business.ViewModels.Room;

public class CinemaRoomViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SeatRows { get; set; }
    public int SeatColumns { get; set; }
    public int Capacity => SeatRows * SeatColumns;
}
