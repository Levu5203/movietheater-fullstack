using MediatR;
using MovieTheater.Business.ViewModels.Movie;

namespace MovieTheater.Business.Handlers.Movie;

public class MovieGetByIdQuery : IRequest<MovieViewModel>
{
    public Guid Id { get; set; }
}