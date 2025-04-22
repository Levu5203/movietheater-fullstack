using Microsoft.AspNetCore.Http;
using MovieTheater.Business.ViewModels.Showtime;

namespace MovieTheater.Business.ViewModels.Movie;

public class MovieAdminViewModel : MasterBaseViewModel
{
    
    public required string Name { get; set; }

    
    public required int Duration { get; set; }

    
    public required string Origin { get; set; }

   
    public required string Description { get; set; }

    
    public required string Version { get; set; }

    
    public required string Director { get; set; }

    
    public required string Actors { get; set; }

    
    public required string Status { get; set; }

    
    public required DateOnly ReleasedDate { get; set; }

    
    public required DateOnly EndDate { get; set; }

    
    public required IFormFile PosterImage { get; set; }

   
    public required Guid CinemaRoomId { get; set; }

    
    public required List<string> SelectedGenres { get; set; }

     public required List<ShowtimeViewModel> Showtimes { get; set; } = [];


    public required List<Guid> SelectedShowTimeSlots { get; set; }
}
