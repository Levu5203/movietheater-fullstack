using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using MovieTheater.Business.Services;
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
    private readonly IAzureService _azureService;

    public CreateMovieCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IAzureService azureService)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
        _azureService = azureService;
    }

    public async Task<Guid> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync(); // Start transaction

        try
        {
            // Upload poster
            var fileName = $"/{Guid.NewGuid()}_{request.PosterImage.FileName}";
            var avatarUrl = await _azureService.UploadFileAsync(request.PosterImage, fileName);

            // Create movie
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
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
                PosterUrl = avatarUrl,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.MovieRepository.AddAsync(movie);

            // Add movie genres
            var movieGenres = new List<MovieGenre>();
            foreach (var genreName in request.SelectedGenres)
            {
                var genreType = Enum.Parse<GenreType>(genreName);
                var genre = _unitOfWork.GenreRepository.GetQuery(g => g.Type == genreType).FirstOrDefault();
                if (genre != null)
                {
                    movieGenres.Add(new MovieGenre
                    {
                        MovieId = movie.Id,
                        GenreId = genre.Id
                    });
                }
            }

            if (movieGenres.Any())
                await _unitOfWork.MovieGenreRepository.AddAsync(movieGenres);

            // Create showtimes
            var showTimes = new List<ShowTime>();
            var timeSlots = _unitOfWork.ShowtimeSlotRepository.GetQuery().ToList();
            var currentDate = request.ReleasedDate;
            int count = 0;

            while (currentDate <= request.EndDate)
            {
                foreach (var timeSlotId in request.SelectedShowTimeSlots)
                {
                    var slot = timeSlots.FirstOrDefault(x => x.Id == timeSlotId);
                    if (slot == null) continue;

                    var showDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day)
                                       .Add(slot.Time);
                    if (showDateTime <= DateTime.Now) continue;

                    var showTime = new ShowTime
                    {
                        ShowDate = currentDate,
                        MovieId = movie.Id,
                        Movie = movie,
                        CinemaRoomId = request.CinemaRoomId,
                        ShowTimeSlotId = timeSlotId,
                        ShowTimeSlot = slot,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    };

                    if (!HasTimeConflict(showTime.CinemaRoomId, showTime.ShowDate, slot.Time, movie.Duration, showTime.Id, showTimes))
                    {
                        showTimes.Add(showTime);
                        count++;
                    }
                }

                currentDate = currentDate.AddDays(1);
            }

            if (!showTimes.Any())
                throw new InvalidOperationException("Selected showtimes are not available for the selected room. Try another room or showtimes.");

            await _unitOfWork.ShowtimeRepository.AddAsync(showTimes);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(); // Commit everything

            return movie.Id;
        }
        catch
        {
            await transaction.RollbackAsync(); // Rollback all changes
            throw;
        }
    }


    private bool HasTimeConflict(
    Guid roomId,
    DateOnly showDate,
    TimeSpan startTime,
    int duration,
    Guid currentShowTimeId,
    List<ShowTime>? newShowTimes = null)
    {
        var showTimesInRoom = _unitOfWork.ShowtimeRepository.GetQuery(false)
            .Where(s => s.CinemaRoomId == roomId && s.ShowDate == showDate && s.Id != currentShowTimeId)
            .Select(s => new
            {
                s.ShowTimeSlot!.Time,
                s.Movie!.Duration
            })
            .ToList();

        var endTime = startTime.Add(TimeSpan.FromMinutes(duration));

        foreach (var existingShowTime in showTimesInRoom)
        {
            var existingEndTime = existingShowTime.Time.Add(TimeSpan.FromMinutes(existingShowTime.Duration));

            if ((startTime >= existingShowTime.Time && startTime < existingEndTime) ||
                (endTime > existingShowTime.Time && endTime <= existingEndTime) ||
                (startTime <= existingShowTime.Time && endTime >= existingEndTime))
            {
                Console.WriteLine($"Time conflict (DB) for showtime: {currentShowTimeId}, at Room: {roomId} : {startTime}");
                return true;
            }
        }

        if (newShowTimes != null)
        {
            foreach (var existingShowTime in newShowTimes.Where(s => s.ShowDate == showDate))
            {
                var existingStartTime = existingShowTime.ShowTimeSlot.Time;
                var existingEndTime = existingStartTime.Add(TimeSpan.FromMinutes(existingShowTime.Movie.Duration));

                if ((startTime >= existingStartTime && startTime < existingEndTime) ||
                    (endTime > existingStartTime && endTime <= existingEndTime) ||
                    (startTime <= existingStartTime && endTime >= existingEndTime))
                {
                    Console.WriteLine($"Time conflict (NEW) for showtime: {currentShowTimeId}, at Room: {roomId} : {startTime}");
                    return true;
                }
            }
        }

        return false;
    }

}