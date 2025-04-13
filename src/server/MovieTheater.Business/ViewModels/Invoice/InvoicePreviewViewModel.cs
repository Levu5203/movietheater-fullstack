using System;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Business.ViewModels.Ticket;

namespace MovieTheater.Business.ViewModels.Invoice;

public class InvoicePreviewViewModel : MasterBaseViewModel
{
    public Guid UserId { get; set; }
    public double TotalMoney { get; set; }
    public int AddedScore { get; set; }
    public required List<TicketViewModel> Tickets { get; set; }
    public required string RoomName { get; set; }
    public required string MovieName { get; set; }
    public required DateOnly ShowDate { get; set; }
    public required TimeSpan StartTime { get; set; }
    public required string ShowTimeId { get; set; }
}
