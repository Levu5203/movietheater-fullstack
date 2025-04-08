using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MovieTheater.Business.Handlers.Movie;

public class UpdateMovieQuery : IRequest<bool>
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Duration { get; set; }
    
    public string? Origin { get; set; }
    
    public string? Description { get; set; }
    
    public string? Version { get; set; }
    
    public string? Director { get; set; }
    
    public string? Actors { get; set; }

    public string? Status { get; set; }
    
    public DateOnly ReleasedDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public IFormFile? PosterImage { get; set; }

    public Guid CinemaRoomId { get; set; }

    public List<string>? SelectedGenres { get; set; }

    public List<Guid>? SelectedShowTimeSlots { get; set; }
}
