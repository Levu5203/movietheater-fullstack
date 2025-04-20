using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Movie;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Movie;

public class MovieGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler(unitOfWork, mapper),
                                                            IRequestHandler<MovieGetByIdQuery, MovieViewModel>
{
    public async Task<MovieViewModel> Handle(MovieGetByIdQuery request, CancellationToken cancellationToken){
        var movie = await _unitOfWork.MovieRepository.GetQuery().Include(m => m.ShowTimes)
        .ThenInclude(st => st.CinemaRoom)
        .Include(m => m.ShowTimes)
        .ThenInclude(st => st.ShowTimeSlot)
        .Include(m => m.MovieGenres)
        .ThenInclude(mg => mg.Genre).FirstOrDefaultAsync(m => m.Id ==request.Id, cancellationToken);
        return _mapper.Map<MovieViewModel>(movie);
    }
}
