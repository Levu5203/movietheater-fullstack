using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("Seats", Schema = "Common")]
public class Seat : MasterDataBaseEntity, IMasterDataBaseEntity
{
    [Required(ErrorMessage = "Row is required")]
    public required char Row { get; set; }

    [Required(ErrorMessage = "Column is required")]
    public required int Column { get; set; }

    [Required(ErrorMessage = "SeatType is required")]
    [EnumDataType(typeof(SeatType), ErrorMessage = "Invalid seat type")]
    public required SeatType SeatType { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = [];

    [Required(ErrorMessage = "CinemaRoomId is required")]
    public required Guid CinemaRoomId { get; set; }

    [ForeignKey(nameof(CinemaRoomId))]
    public virtual CinemaRoom? CinemaRoom { get; set; }
}
