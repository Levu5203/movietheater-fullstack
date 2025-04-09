using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieTheater.Models.Security;

namespace MovieTheater.Models.Common;

[Table("Genres", Schema = "Common")]
public class Genre : MasterDataBaseEntity, IMasterDataBaseEntity
{

    [EnumDataType(typeof(GenreType), ErrorMessage = "Invalid Type")]
    [StringLength(50, ErrorMessage = "Genre Type can't be longer than 50 characters")]
    public required GenreType Type { get; set; }

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } =[];
}