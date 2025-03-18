using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("Genres", Schema = "Common")]
public class Genre
{
    [Required(ErrorMessage = "GenreId is required")]
    public required Guid GenreId { get; set; }

    [EnumDataType(typeof(GenreType), ErrorMessage = "Invalid Type")]
    [StringLength(50, ErrorMessage = "Genre Type can't be longer than 50 characters")]
    public required GenreType Type { get; set; }

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } =[];
}