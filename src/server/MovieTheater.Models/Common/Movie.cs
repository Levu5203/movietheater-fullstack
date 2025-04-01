using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTheater.Models.Common;

[Table("Movies", Schema = "Common")]
public class Movie : MasterDataBaseEntity, IMasterDataBaseEntity
{      
    [Required(ErrorMessage = "Name is required")]
    [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 600, ErrorMessage = "Duration must be between 1 and 600 minutes")]
    public required int Duration { get; set; }

    [Required(ErrorMessage = "Origin is required")]
    [StringLength(50, ErrorMessage = "Origin can't be longer than 50 characters")]
    public required string Origin { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Version is required")]
    [EnumDataType(typeof(VersionType), ErrorMessage = "Invalid Version")]
    public required VersionType Version { get; set; }

    [Required(ErrorMessage = "PosterUrl is required")]
    [Url(ErrorMessage = "Invalid PosterUrl")]
    [StringLength(255, ErrorMessage = "PosterUrl can't be longer than 255 characters")]
    public required string PosterUrl { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(MovieStatus), ErrorMessage = "Invalid Movie Status")]
    public required MovieStatus Status { get; set; }

    [Required(ErrorMessage = "ReleasedDate is required")]
    public required DateOnly ReleasedDate { get; set; }
    [Required(ErrorMessage = "Director is required")]
    [StringLength(255, ErrorMessage = "Director can't be longer than 255 characters")]
    public required string Director {get; set;}
    [Required(ErrorMessage = "Actors is required")]
    [StringLength(255, ErrorMessage = "Actors can't be longer than 255 characters")]
    public required string Actors {get; set;}

    public virtual ICollection<ShowTime> ShowTimes { get; set; } = [];

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = [];
}
