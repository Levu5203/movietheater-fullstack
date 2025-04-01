using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Room
{
    public class SeatViewModel
    {
        public Guid Id { get; set; }
        public Guid CinemaRoomId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public SeatType SeatType { get; set; }
    }
}