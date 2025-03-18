using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("CinemaRooms", Schema = "Common")]
public class CinemaRoom : MasterDataBaseEntity, IMasterDataBaseEntity
{   
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "SeatRows is required")]
    public int SeatRows { get; set; }

    [Required(ErrorMessage = "SeatColumns is required")]
    public int SeatColumns { get; set; }    
    public virtual ICollection<Seat> Seats { get; set; } = [];
    public virtual ICollection<ShowTime> ShowTimes { get; set; } = [];
}
