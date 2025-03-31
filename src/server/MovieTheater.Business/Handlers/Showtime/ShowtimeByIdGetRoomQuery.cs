using System;
using MediatR;
using MovieTheater.Business.ViewModels.CinemaRoom;

namespace MovieTheater.Business.Handlers.Showtime;

public class ShowtimeByIdGetRoomQuery : IRequest<CinemaViewModel>
{
    public Guid ShowTimeId { get; set; }
     public ShowtimeByIdGetRoomQuery(Guid showTimeId)
    {
        ShowTimeId = showTimeId;
    }
}
