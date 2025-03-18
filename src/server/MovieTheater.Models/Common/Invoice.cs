using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieTheater.Models.Security;

namespace MovieTheater.Models.Common;

[Table("Invoices", Schema = "Common")]
public class Invoice : MasterDataBaseEntity, IMasterDataBaseEntity
{
    [Required(ErrorMessage = "Total money is required")]
    public required double TotalMoney { get; set; }

    [Required(ErrorMessage = "Added Score is required")]
    public required int AddedScore { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    [ForeignKey(nameof(UserId))]
    public required Guid UserId { get; set; }

    [Required(ErrorMessage = "User is required")]
    public virtual required User User { get; set; }

    [Required(ErrorMessage = "ShowTimeId is required")]
    public required Guid ShowTimeId { get; set; }

    [ForeignKey(nameof(ShowTimeId))]
    public virtual ShowTime? ShowTime { get; set; }

    public virtual ICollection<Ticket>? Tickets { get; set; } =[];

    public virtual ICollection<HistoryScore> HistoryScores { get; set; } = [];
}
