using MediatR;
using MovieTheater.Business.ViewModels.Showtime;

namespace MovieTheater.Business.Handlers.Showtime;

public class ShowtimeGetAllQuery : IRequest<IEnumerable<ShowtimeViewModel>>
{
    public Guid? UserId {get; set;}
}
