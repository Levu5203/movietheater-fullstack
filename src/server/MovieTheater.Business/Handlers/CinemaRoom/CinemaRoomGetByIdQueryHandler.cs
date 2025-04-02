using System;
using AutoMapper;
using MediatR;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.CinemaRoom;

public class CinemaRoomGetByIdQueryHandler : BaseHandler, IRequestHandler<CinemaRoomGetByIdQuery, CinemaRoomViewModel>
{
    public CinemaRoomGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<CinemaRoomViewModel> Handle(CinemaRoomGetByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.CinemaRoomRepository.GetByIdAsync(request.Id);
        if (room == null || room.IsDeleted){
            throw new KeyNotFoundException("Cinema Room not found");
        }

        return _mapper.Map<CinemaRoomViewModel>(room);
    }
}
