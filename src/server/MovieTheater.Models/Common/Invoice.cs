using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieTheater.Models.Security;

namespace MovieTheater.Models.Common;

[Table("Invoices", Schema = "Common")]
public class Invoice : MasterDataBaseEntity
{
    [Required(ErrorMessage = "Total money is required")]
    public required decimal TotalMoney { get; set; }

    [Required(ErrorMessage = "Added Score is required")]
    public required int AddedScore { get; set; }

    public bool TicketIssued { get; set; } = false;

    [Required(ErrorMessage = "UserId is required")]
    public required Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    [Required(ErrorMessage = "ShowTimeId is required")]
    public required Guid ShowTimeId { get; set; }

    [ForeignKey(nameof(ShowTimeId))]
    public virtual ShowTime? ShowTime { get; set; }

    [Required(ErrorMessage = "CinemaRoomId is required")]
    public required Guid CinemaRoomId { get; set; }

    [ForeignKey(nameof(CinemaRoomId))]
    public virtual CinemaRoom? CinemaRoom { get; set; }

    [Required(ErrorMessage = "MovieId is required")]
    public required Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = [];

    public virtual ICollection<HistoryScore> HistoryScores { get; set; } = new HashSet<HistoryScore>();
}