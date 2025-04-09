using MediatR;
using MovieTheater.Business.ViewModels.Seat;

namespace MovieTheater.Business.Handlers.Seat
{
    public class SeatGetByRoomIdQuery : IRequest<IEnumerable<SeatViewModel>>
    {
        public Guid RoomId { get; set; }
    }
}