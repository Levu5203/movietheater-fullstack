using System;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Seat;

public class SeatShowTimeViewModel
{
    public required Guid SeatShowTimeId { get; set; }
    public required Guid SeatId { get; set; }
    public required Guid ShowTimeId { get; set; }
    public required SeatStatus Status { get; set; }
}
