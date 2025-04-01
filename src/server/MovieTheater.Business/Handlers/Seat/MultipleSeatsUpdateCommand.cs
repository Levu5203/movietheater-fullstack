using MediatR;
using MovieTheater.Business.ViewModels.Room;

namespace MovieTheater.Business.Handlers.Seat
{
    public class MultipleSeatsUpdateCommand : IRequest<IEnumerable<SeatViewModel>>
    {
        public List<SeatUpdateCommand> Commands { get; set; } = [];
    }
}
