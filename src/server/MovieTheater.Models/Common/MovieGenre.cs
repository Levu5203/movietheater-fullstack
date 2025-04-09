using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("MovieGenres", Schema = "Common")]
public class MovieGenre : BaseEntity
{
    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Movie? Movie { get; set; }
    public Guid GenreId { get; set; } 

    [ForeignKey(nameof(GenreId))]
    public Genre? Genre { get; set; }

}
