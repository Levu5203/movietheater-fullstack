using System;
using MediatR;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Showtime;

public class GetAllSeatShowtimeQuery : IRequest<IEnumerable<SeatShowTimeViewModel>>
{
    public Guid ShowtimeId { get; set; }
    
}
