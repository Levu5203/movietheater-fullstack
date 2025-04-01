using MediatR;
using MovieTheater.Business.ViewModels.Room;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.UnitOfWorks;
using AutoMapper;

namespace MovieTheater.Business.Handlers.CinemaRoom;

public class CinemaRoomsGetAllQueryHandler : BaseHandler, IRequestHandler<CinemaRoomsGetAllQuery, IEnumerable<CinemaRoomViewModel>>
{
    public CinemaRoomsGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<IEnumerable<CinemaRoomViewModel>> Handle(CinemaRoomsGetAllQuery request, CancellationToken cancellationToken)
    {
        var cinemaRooms = await _unitOfWork.CinemaRoomRepository
            .GetQuery(true)
            .Select(room => new CinemaRoomViewModel
            {
                Id = room.Id,
                Name = room.Name,
                SeatRows = room.SeatRows,
                SeatColumns = room.SeatColumns,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return cinemaRooms;
    }
}
