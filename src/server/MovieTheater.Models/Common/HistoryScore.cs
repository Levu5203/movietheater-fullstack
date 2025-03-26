using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("HistoryScores", Schema = "Common")]
public class HistoryScore : BaseEntity
{   
    [Required(ErrorMessage = "UserId is required")]
    public required int Score { get; set; }

    [Required(ErrorMessage = "ScoreStatus is required")]
    public required ScoreStatus ScoreStatus { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(255, ErrorMessage = "Description can't be longer than 50 characters")]
    public required  string Description { get; set; }

    [Required(ErrorMessage = "InvoiceId is required")]
    public required Guid InvoiceId { get; set; }

    [ForeignKey(nameof(InvoiceId))]
    public virtual required Invoice Invoice { get; set; }
}
