using System;

namespace MovieTheater.Business.ViewModels.Seat;

public class SeatViewModel : MasterBaseViewModel
{
    public required char Row { get; set; }
    public required int Column { get; set; }
    public required int SeatType { get; set; }
    public required Guid RoomId { get; set; }
}
