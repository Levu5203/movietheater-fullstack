using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("ShowTimeSlots", Schema = "Common")]
public class ShowTimeSlot : BaseEntity
{   
    [Required(ErrorMessage = "ShowTimeSlotId is required")]
    public required Guid ShowTimeSlotId { get; set; }

    [Required(ErrorMessage = "Time is required")]
    public required TimeSpan Time { get; set; }

    public virtual  ICollection<ShowTime> ShowTimes { get; set; } = [];
}
