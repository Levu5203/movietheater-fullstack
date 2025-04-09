using System;
using MediatR;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Movie;

public class DeleteMovieCommandHandler: IRequestHandler<DeleteMovieCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovieCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _unitOfWork.MovieRepository.GetByIdAsync(request.Id);
        if (movie == null)
            return false; // Trả về false nếu không tìm thấy

        _unitOfWork.MovieRepository.Delete(movie);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
