using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("Tickets", Schema = "Common")]
public class Ticket : MasterDataBaseEntity, IMasterDataBaseEntity
{
    [Required(ErrorMessage = "Price is required")]
    public required decimal Price { get; set; }

    [Required(ErrorMessage = "BookingDate is required")]
    public required DateTime BookingDate { get; set; }

    [Required(ErrorMessage = "SeatId is required")]
    public required Guid SeatId { get; set; }

    [ForeignKey(nameof(SeatId))]
    public virtual Seat? Seat { get; set; }

    [Required(ErrorMessage = "RoomId is required")]
    public required Guid CinemaRoomId { get; set; }

    [ForeignKey(nameof(CinemaRoomId))]
    public virtual CinemaRoom? CinemaRoom { get; set; }

    [Required(ErrorMessage = "ShowTimeId is required")]
    public required Guid ShowTimeId { get; set; }

    [ForeignKey(nameof(ShowTimeId))]
    public virtual ShowTime? ShowTime { get; set; }

    [Required(ErrorMessage = "MovieId is required")]
    public required Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie? Movie { get; set; }

    [Required(ErrorMessage = "InvoiceId is required")]
    public required Guid InvoiceId { get; set; }

    [ForeignKey(nameof(InvoiceId))]
    public virtual Invoice? Invoice { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(TicketStatus), ErrorMessage = "Invalid ticket status")]
    public required TicketStatus Status { get; set; }

    public Guid? PromotionId { get; set; }

    [ForeignKey(nameof(PromotionId))]
    public virtual Promotion? Promotion { get; set; }

    public enum TicketStatus
    {
        Paid = 1,
        Issued = 2,
        Used = 3,
    }
}
