using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MovieTheater.ViewModels;

public class CreateMovieViewModel
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
    public required string Version { get; set; }

    [Required(ErrorMessage = "Director is required")]
    [StringLength(255, ErrorMessage = "Director can't be longer than 255 characters")]
    public required string Director { get; set; }

    [Required(ErrorMessage = "Actors is required")]
    [StringLength(255, ErrorMessage = "Actors can't be longer than 255 characters")]
    public required string Actors { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public required string Status { get; set; }

    [Required(ErrorMessage = "ReleasedDate is required")]
    public required DateOnly ReleasedDate { get; set; }

    [Required(ErrorMessage = "EndDate is required")]
    public required DateOnly EndDate { get; set; }

    [Required(ErrorMessage = "Movie Poster is required")]
    public required IFormFile PosterImage { get; set; }

    [Required(ErrorMessage = "Cinema Room is required")]
    public required Guid CinemaRoomId { get; set; }

    [Required(ErrorMessage = "At least one genre must be selected")]
    public required List<string> SelectedGenres { get; set; }

    [Required(ErrorMessage = "At least one schedule must be selected")]
    public required List<Guid> SelectedShowTimeSlots { get; set; }
}