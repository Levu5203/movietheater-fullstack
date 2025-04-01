using MediatR;
using MovieTheater.Business.ViewModels.Seat;

namespace MovieTheater.Business.Handlers.Showtime;

public class GetSeatsByShowTimeIdQuery : IRequest<IEnumerable<SeatViewModel>>
{
    public Guid ShowTimeId { get; set; }
}
