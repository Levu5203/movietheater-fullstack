using MediatR;
using MovieTheater.Business.ViewModels.Invoice;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatPaymentEmployeeCommand : IRequest<InvoicePreviewViewModel>
{
    public Guid InvoiceId { get; set; }
    public string? PhoneNumber { get; set; }
}
