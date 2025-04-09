using MovieTheater.Business.ViewModels.Invoice;

namespace MovieTheater.Business.Handlers.Invoice;

public class InvoiceSearchQuery: MasterDataSearchQuery<InvoiceViewModel>
{
    public bool? TicketIssued { get; set; }
}
