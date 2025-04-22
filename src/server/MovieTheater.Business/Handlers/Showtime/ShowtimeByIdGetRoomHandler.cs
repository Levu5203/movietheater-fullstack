using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.CinemaRoom;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Showtime;

public class ShowtimeByIdGetRoomHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler(unitOfWork, mapper),
                                                            IRequestHandler<ShowtimeByIdGetRoomQuery, CinemaViewModel>
{
    public async Task<CinemaViewModel> Handle(ShowtimeByIdGetRoomQuery request, CancellationToken cancellationToken)
    {
         // Lấy thông tin Showtime cùng với Room
        var showtime = await _unitOfWork.ShowtimeRepository.GetQuery()
            .Include(st => st.CinemaRoom)
            .FirstOrDefaultAsync(st => st.Id == request.ShowTimeId, cancellationToken);

        if (showtime == null || showtime.CinemaRoom == null)
        {
            throw new KeyNotFoundException($"Showtime or Room not found for ID {request.ShowTimeId}");
        }
        
        return _mapper.Map<CinemaViewModel>(showtime);
    }
}
