using System;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.ViewModels.Movie;

public class MovieViewModel : MasterBaseViewModel
{
    public Guid Id {get; set;}
    public required string Name {get; set;}
    public int Duration {get; set;}
    public string? Origin {get; set;}
    public string? Description {get; set;}
    public VersionType Version {get; set;}
    public string? PosterUrl {get; set;}
    public MovieStatus Status {get; set;}
    public DateTime ReleaseDate {get; set;}
}
