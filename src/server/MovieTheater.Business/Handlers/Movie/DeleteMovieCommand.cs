using MediatR;

namespace MovieTheater.Business.Handlers.Movie;

public class DeleteMovieCommand: IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteMovieCommand(Guid id)
    {
        Id = id;
    }
}
