using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("Tickets", Schema = "Common")]
public class Ticket : MasterDataBaseEntity, IMasterDataBaseEntity
{
    [Required(ErrorMessage = "Seat is required")]
    public required double Price { get; set; }

    [Required(ErrorMessage = "BookingDate is required")]
    public required DateTime BookingDate { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(TicketStatus), ErrorMessage = "Invalid ticket status")]
    public required TicketStatus Status { get; set; }

    [Required(ErrorMessage = "InvoiceId is required")]
    public required Guid InvoiceId { get; set; }

    [ForeignKey(nameof(InvoiceId))]
    public virtual Invoice? Invoice { get; set; }

    [Required(ErrorMessage = "Seat is required")]
    public required Guid SeatId { get; set; }

    [ForeignKey(nameof(SeatId))]
    public virtual Seat? Seat { get; set; }
    public Guid? PromotionId { get; set; }

    [ForeignKey(nameof(PromotionId))]
    public virtual Promotion? Promotion { get; set; }
}
