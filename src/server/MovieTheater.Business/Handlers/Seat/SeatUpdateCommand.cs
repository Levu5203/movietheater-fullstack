// Business/Seats/Commands/UpdateSeatTypeCommand.cs
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Seat
{
    public class SeatUpdateCommand : MasterBaseUpdateCommand<SeatViewModel>
    {
        public SeatType NewType { get; set; }
    }
}