using MediatR;
using MovieTheater.Business.ViewModels.Movie;

namespace MovieTheater.Business.Handlers.Movie;

public class MovieGetAllQuery : IRequest<IEnumerable<MovieViewModel>>
{
    public Guid? UserId { get; set; }
}
