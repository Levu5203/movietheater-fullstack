using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Core.Exceptions;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Showtime;
public class GetSeatsByShowTimeIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler(unitOfWork, mapper),
                IRequestHandler<GetSeatsByShowTimeIdQuery, IEnumerable<SeatViewModel>>
{
    public async Task<IEnumerable<SeatViewModel>> Handle(GetSeatsByShowTimeIdQuery request, CancellationToken cancellationToken)
    {
        var showtime = await _unitOfWork.ShowtimeRepository.GetQuery()
            .Include(st => st.CinemaRoom)
                .ThenInclude(r => r.Seats)
            .FirstOrDefaultAsync(st => st.Id == request.ShowTimeId, cancellationToken);

        if (showtime == null || showtime.CinemaRoom == null)
        {
            throw new ResourceNotFoundException($"Showtime or Room not found for ID {request.ShowTimeId}");
        }
        var availableSeats = showtime.CinemaRoom.Seats
            .Where(seat => seat.IsActive) 
            .ToList();
        return _mapper.Map<IEnumerable<SeatViewModel>>(availableSeats);
    }
}
