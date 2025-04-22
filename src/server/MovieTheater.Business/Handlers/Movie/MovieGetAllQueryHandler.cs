using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Movie;

public class MovieGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseHandler(unitOfWork, mapper), IRequestHandler<MovieGetAllQuery, IEnumerable<MovieViewModel>>
{
    public async Task<IEnumerable<MovieViewModel>> Handle(MovieGetAllQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.MovieRepository.GetQuery()
              .Include(m => m.ShowTimes)
                  .ThenInclude(st => st.CinemaRoom)
              .Include(m => m.ShowTimes)
                  .ThenInclude(st => st.ShowTimeSlot)
              .Include(m => m.MovieGenres)
                  .ThenInclude(mg => mg.Genre);
        var movies = await query.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<MovieViewModel>>(movies);
    }
}
