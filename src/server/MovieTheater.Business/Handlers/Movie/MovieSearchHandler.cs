using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.Handlers.Movie;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Core;
using MovieTheater.Core.Extensions;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Movies;

public class MovieSearchQueryHandler :
    BaseHandler,
    IRequestHandler<MovieSearchQuery, PaginatedResult<MovieViewModel>>
{
    public MovieSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) {}

    public async Task<PaginatedResult<MovieViewModel>> Handle(MovieSearchQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.MovieRepository.GetQuery()
            .Include(m => m.ShowTimes)
                .ThenInclude(st => st.CinemaRoom)
            .Include(m => m.ShowTimes)
                .ThenInclude(st => st.ShowTimeSlot)
            .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
            .AsQueryable();

        // Filter by keyword
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            var keywordLower = request.Keyword.ToLower();

            query = query.Where(m =>
                m.Name.ToLower().Contains(keywordLower) ||
                m.Director.ToLower().Contains(keywordLower) ||
                m.Actors.ToLower().Contains(keywordLower)
            );
        }

        // Count total
        var total = await query.CountAsync(cancellationToken);

        // Sorting
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = query.OrderByExtension(request.OrderBy, request.OrderDirection.ToString());
        }
        else
        {
            query = query.OrderBy(m => m.Name); // Default sort
        }

        // Pagination
        var items = await query
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<IEnumerable<MovieViewModel>>(items);

        return new PaginatedResult<MovieViewModel>(request.PageNumber, request.PageSize, total, viewModels);
    }
}
