using System;
using MediatR;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Business.ViewModels.Ticket;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatReverveCommand : IRequest<InvoicePreviewViewModel>
{
    public Guid ShowTimeId { get; set; }
    public required List<Guid> SeatIds { get; set; } = [];
}
