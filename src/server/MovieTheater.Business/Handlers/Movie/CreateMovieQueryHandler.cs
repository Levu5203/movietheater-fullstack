using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MovieTheater.Commands;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;
using System.IO;
using System.Linq.Expressions;

namespace MovieTheater.Handlers;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateMovieCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Guid> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        // Handle image upload
        string posterUrl = await UploadPosterImage(request.PosterImage);

        // Create movie
        var movie = new Movie
        {
            Name = request.Name,
            Duration = request.Duration,
            Origin = request.Origin,
            Description = request.Description,
            Version = Enum.Parse<VersionType>(request.Version),
            Status = Enum.Parse<MovieStatus>(request.Status),
            Director = request.Director,
            Actors = request.Actors,
            ReleasedDate = request.ReleasedDate,
            EndDate = request.EndDate,
            PosterUrl = posterUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Add movie using the generic repository
        await _unitOfWork.MovieRepository.AddAsync(movie);
        await _unitOfWork.SaveChangesAsync();

        // Add movie genres
        var movieGenres = new List<MovieGenre>();
        foreach (var genreName in request.SelectedGenres)
        {
            // Using the generic repository's GetQuery method with an expression
            var genreType = Enum.Parse<GenreType>(genreName);
            var genre = _unitOfWork.GenreRepository.GetQuery(g => g.Type == genreType).FirstOrDefault();
            if (genre != null)
            {
                var movieGenre = new MovieGenre
                {
                    MovieId = movie.Id,
                    GenreId = genre.Id
                };
                movieGenres.Add(movieGenre);
            }
        }
        
        // Add all movie genres at once
        if (movieGenres.Any())
        {
            await _unitOfWork.MovieGenreRepository.AddAsync(movieGenres);
        }

        // Create showtimes for each day between ReleasedDate and EndDate
        var showTimes = new List<ShowTime>();
        var currentDate = request.ReleasedDate;
        while (currentDate <= request.EndDate)
        {
            foreach (var timeSlotId in request.SelectedShowTimeSlots)
            {
                var showTime = new ShowTime
                {
                    ShowDate = currentDate,
                    MovieId = movie.Id,
                    CinemaRoomId = request.CinemaRoomId,
                    ShowTimeSlotId = timeSlotId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                showTimes.Add(showTime);
            }
            currentDate = currentDate.AddDays(1);
        }

        // Add all showtimes at once
        if (showTimes.Any())
        {
            await _unitOfWork.ShowtimeRepository.AddAsync(showTimes);
        }

        await _unitOfWork.SaveChangesAsync();
        return movie.Id;
    }

    private async Task<string> UploadPosterImage(IFormFile posterImage)
    {
        // Create upload folder if it doesn't exist
        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/posters");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // Create unique filename
        string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(posterImage.FileName)}";
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Save file
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await posterImage.CopyToAsync(fileStream);
        }

        // Return relative URL path
        return $"/uploads/posters/{uniqueFileName}";
    }
}