using MediatR;
using MovieTheater.Business.ViewModels.Invoice;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatReverveCommand : IRequest<InvoicePreviewViewModel>
{
    public Guid ShowTimeId { get; set; }
    public required List<Guid> SeatIds { get; set; } = [];
}
