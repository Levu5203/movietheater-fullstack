using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("TicketShowTimeMovies", Schema = "Common")]
public class TicketShowtimeMovie
{   
    [Required(ErrorMessage = "TicketShowTimeMovie is required")]
    public required Guid TicketShowTimeMovieId { get; set; }   

    [ForeignKey(nameof(TicketId))]
    public Guid TicketId { get; set; }

    [ForeignKey(nameof(ShowTimeId))]
    public Guid ShowTimeId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Guid MovieId { get; set; }
    
    public required Guid SeatId {get; set;}
    public required Guid RoomId {get; set;}

}