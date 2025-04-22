using MediatR;
using MovieTheater.Business.ViewModels.Invoice;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatPaymentCommand : IRequest<InvoicePreviewViewModel>
{
    public Guid InvoiceId { get; set; }
}
