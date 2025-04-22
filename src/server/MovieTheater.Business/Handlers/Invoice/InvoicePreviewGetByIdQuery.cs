using MediatR;
using MovieTheater.Business.ViewModels.Invoice;

namespace MovieTheater.Business.Handlers.Invoice;

public class InvoicePreviewGetByIdQuery : IRequest<InvoicePreviewViewModel>
{
    public Guid InvoiceId { get; set; }
}