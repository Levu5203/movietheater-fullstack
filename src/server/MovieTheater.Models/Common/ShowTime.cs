using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace MovieTheater.Models.Common;

[Table("ShowTimes", Schema = "Common")]
public class ShowTime : MasterDataBaseEntity, IMasterDataBaseEntity
{   
    [Required(ErrorMessage = "ShowDate is required")]
    public required DateOnly ShowDate { get; set; }

    [Required(ErrorMessage = "ShowTimeSlotId is required")]
    public required Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie? Movie { get; set; }

    [Required(ErrorMessage = "CinemaRoomId is required")]
    public required Guid CinemaRoomId { get; set; }

    [ForeignKey(nameof(CinemaRoomId))]
    public virtual CinemaRoom? CinemaRoom { get; set; }

    [Required(ErrorMessage = "ShowTimeSlotId is required")]
    public required Guid ShowTimeSlotId { get; set; }

    [ForeignKey(nameof(ShowTimeSlotId))]
    public virtual ShowTimeSlot? ShowTimeSlot { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = [];

    [NotMapped]
    public TimeSpan EndTime
    {
        get
        {
            if (ShowTimeSlot == null || Movie == null)
                return TimeSpan.Zero;

            return ShowTimeSlot.Time.Add(TimeSpan.FromMinutes(Movie.Duration));
        }
    }
}
