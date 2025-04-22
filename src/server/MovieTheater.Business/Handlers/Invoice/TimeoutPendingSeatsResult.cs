namespace MovieTheater.Business.Handlers.Invoice;

public class TimeoutPendingSeatsResult
{
    public int ProcessedInvoicesCount { get; set; }
    public int ResetSeatsCount { get; set; }
    public int RemovedTicketsCount { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<Guid> ProcessedInvoiceIds { get; set; } = new List<Guid>();
}
