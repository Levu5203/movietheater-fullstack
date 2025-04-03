using System;
using MovieTheater.Models.Common;


namespace MovieTheater.Business.ViewModels.Ticket;


public class BookedTicketViewModel
{
    public required string MovieName { get; set; }
    public required DateTime BookingDate { get; set; }
    public required double TotalMoney { get; set; }
    public required TicketStatus Status { get; set; }
}
