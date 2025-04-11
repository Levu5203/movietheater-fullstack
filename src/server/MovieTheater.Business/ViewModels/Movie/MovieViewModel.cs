using System;
using MovieTheater.Business.ViewModels.Showtime;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Movie;

public class MovieViewModel : MasterBaseViewModel
{
    public required string Name { get; set; }
    public int Duration { get; set; }
    public string? Origin { get; set; }
    public string? Description { get; set; }
    public required string Director { get; set; }
    public required string Actors { get; set; }
    public VersionType Version { get; set; }
    public string? PosterUrl { get; set; }
    public MovieStatus Status { get; set; }
    public DateOnly ReleasedDate { get; set; }
    public required List<ShowtimeViewModel> Showtimes { get; set; } = [];

    public required List<string> Genres { get; set; } = [];
    public required List<string> CinemaRooms { get; set; } = [];
}
