using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Movie;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, bool>
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
            return false;

        // Lấy tất cả showtime của movie
        var showtimes = await _unitOfWork.ShowtimeRepository
            .GetQuery(false).Where(s => s.MovieId == request.Id).ToListAsync();


        if (showtimes.Any())
        {
            var showtimeIds = showtimes.Select(s => s.Id).ToList();

            // Kiểm tra nếu có ticket nào tồn tại với showtimeId trong danh sách này
            var hasTickets = await _unitOfWork.TicketRepository.GetQuery(false)
                .AnyAsync(t => showtimeIds.Contains(t.ShowTimeId));

            if (hasTickets)
            {
                // Movie có vé bán rồi → không được xoá
                throw new InvalidOperationException("Cannot delete this movie because one or more showtimes already have sold tickets.");
            }
        }

        // Không có vé bán → được xoá
        System.Console.WriteLine("Xoa duoc");
        _unitOfWork.MovieRepository.Delete(movie);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

}
