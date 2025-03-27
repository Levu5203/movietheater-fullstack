using System;


namespace MovieTheater.Business.ViewModels.Ticket;


public class BookedTicketViewModel
{
    public required string MovieName { get; set; }
    public required DateTime BookingDate { get; set; }
    public required double TotalMoney { get; set; }
    public required string Status { get; set; }
}
