using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Seat
{
    public class SeatGetByRoomIdQueryHandler : BaseHandler, IRequestHandler<SeatGetByRoomIdQuery, IEnumerable<SeatViewModel>>
    {

        public SeatGetByRoomIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<SeatViewModel>> Handle(SeatGetByRoomIdQuery request, CancellationToken cancellationToken)
        {
            var seats = await _unitOfWork.SeatRepository.GetQuery().Where(s => s.CinemaRoomId == request.RoomId).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<SeatViewModel>>(seats);
        }
    }
}