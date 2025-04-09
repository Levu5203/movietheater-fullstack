using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Seat
{
    public class SeatUpdateCommand : MasterBaseUpdateCommand<SeatViewModel>
    {
        public SeatType NewType { get; set; }
    }
}