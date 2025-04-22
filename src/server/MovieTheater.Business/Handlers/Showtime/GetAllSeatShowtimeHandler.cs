using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Showtime;

public class GetAllSeatShowtimeHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler(unitOfWork, mapper),
    IRequestHandler<GetAllSeatShowtimeQuery, IEnumerable<SeatShowTimeViewModel>> 
{
    public async Task<IEnumerable<SeatShowTimeViewModel>> Handle(GetAllSeatShowtimeQuery request, CancellationToken cancellationToken)
    {
        var seatShowtimes = await unitOfWork.SeatShowtimeRepository.GetQuery()
            .Where(s => s.ShowTimeId == request.ShowtimeId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<SeatShowTimeViewModel>>(seatShowtimes);
    }
}
