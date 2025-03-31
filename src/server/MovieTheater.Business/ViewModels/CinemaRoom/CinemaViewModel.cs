using System;

namespace MovieTheater.Business.ViewModels.CinemaRoom;

public class CinemaViewModel : MasterBaseViewModel
{
    public required string Name {get; set;}
    public int SeatRow {get; set;}
    public int SeatColumn {get; set;}
}
