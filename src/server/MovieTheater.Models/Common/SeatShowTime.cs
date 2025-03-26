using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("SeatShowTimes", Schema = "Common")]
public class SeatShowTime
{   
    [Required(ErrorMessage = "SeatShowTimeId is required")]
    public required Guid SeatShowTimeId { get; set; }

    [Required(ErrorMessage = "SeatId is required")]
    public required Guid SeatId { get; set; }

    [Required(ErrorMessage = "ShowTimeId is required")]
    public required Guid ShowTimeId { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public required SeatStatus Status { get; set; }
}
