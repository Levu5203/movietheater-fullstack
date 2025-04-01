using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Showtime;

public class ShowtimeGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler(unitOfWork, mapper),
                                                            IRequestHandler<ShowtimeGetAllQuery, IEnumerable<ShowtimeViewModel>>
{
    public async Task<IEnumerable<ShowtimeViewModel>> Handle(ShowtimeGetAllQuery request, CancellationToken cancellationToken)
    {

        var query = _unitOfWork.ShowtimeRepository.GetQuery()
            .Include(st => st.Movie)
            .Include(st => st.CinemaRoom)
            .Include(st => st.ShowTimeSlot);

        var showtimes = await query.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ShowtimeViewModel>>(showtimes);
    }
}
