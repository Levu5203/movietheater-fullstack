using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.Handlers.Movie;
using MovieTheater.Business.Services;
using MovieTheater.Data;
using MovieTheater.Models.Common;

namespace MovieTheater.Handlers;

public class UpdateMovieQueryHandler : IRequestHandler<UpdateMovieQuery, bool>
{
    private readonly IAzureService _azureService;
    private readonly MovieTheaterDbContext _context;

    public UpdateMovieQueryHandler(IAzureService azureService, MovieTheaterDbContext context)
    {
        _azureService = azureService;
        _context = context;
    }

    public async Task<bool> Handle(UpdateMovieQuery request, CancellationToken cancellationToken)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var movie = await _context.Movies
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
                if (movie == null)
                {
                    return false;
                }

                if (request.PosterImage != null && request.PosterImage.Length > 0)
                {
                    var fileName = $"/{Guid.NewGuid()}_{request.PosterImage!.FileName}";
                    var posterUrl = await _azureService.UploadFileAsync(request.PosterImage, fileName);
                    movie.PosterUrl = posterUrl;
                }

                movie.Name = request.Name ?? movie.Name;
                movie.Duration = request.Duration;
                movie.Origin = request.Origin ?? movie.Origin;
                movie.Description = request.Description ?? movie.Description;
                movie.Version = request.Version != null ? Enum.Parse<VersionType>(request.Version) : default;
                movie.Status = request.Status != null ? Enum.Parse<MovieStatus>(request.Status) : default;
                movie.Director = request.Director ?? movie.Director;
                movie.Actors = request.Actors ?? movie.Actors;
                movie.ReleasedDate = request.ReleasedDate;
                movie.EndDate = request.EndDate;
                movie.UpdatedAt = DateTime.UtcNow;

                // Xóa tất cả genres cũ
                var oldGenres = await _context.MovieGenres
                    .AsNoTracking()
                    .Where(mg => mg.MovieId == movie.Id)
                    .ToListAsync(cancellationToken);
                if (oldGenres.Any())
                {
                    _context.MovieGenres.RemoveRange(oldGenres);
                    await _context.SaveChangesAsync(cancellationToken);

                    // Ngắt theo dõi tất cả MovieGenre
                    foreach (var entry in _context.ChangeTracker.Entries<MovieGenre>().ToList())
                    {
                        entry.State = EntityState.Detached;
                    }
                }

                // Thêm genres mới
                if (request.SelectedGenres != null && request.SelectedGenres.Any())
                {
                    var newGenreTypes = request.SelectedGenres
                        .Select(g => Enum.Parse<GenreType>(g))
                        .ToList();

                    var allGenres = await _context.Genres
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                    var newGenreIds = allGenres
                        .Where(g => newGenreTypes.Contains(g.Type))
                        .Select(g => g.Id)
                        .ToList();

                    foreach (var genreId in newGenreIds)
                    {
                        _context.MovieGenres.Add(new MovieGenre
                        {
                            MovieId = movie.Id,
                            GenreId = genreId
                        });
                    }
                }

                // Xóa showtimes cũ
                var oldShowTimes = await _context.ShowTimes
                    .AsNoTracking()
                    .Where(s => s.MovieId == movie.Id)
                    .ToListAsync(cancellationToken);
                if (oldShowTimes.Any())
                {
                    _context.ShowTimes.RemoveRange(oldShowTimes);
                    await _context.SaveChangesAsync(cancellationToken);

                    // Ngắt theo dõi tất cả ShowTime
                    foreach (var entry in _context.ChangeTracker.Entries<ShowTime>().ToList())
                    {
                        entry.State = EntityState.Detached;
                    }
                }

                // Tạo showtimes mới
                if (request.SelectedShowTimeSlots != null && request.SelectedShowTimeSlots.Any())
                {
                    var showTimes = new List<ShowTime>();
                    var currentDate = request.ReleasedDate;
                    while (currentDate <= request.EndDate)
                    {
                        foreach (var timeSlotId in request.SelectedShowTimeSlots)
                        {
                            showTimes.Add(new ShowTime
                            {
                                ShowDate = currentDate,
                                MovieId = movie.Id,
                                CinemaRoomId = request.CinemaRoomId,
                                ShowTimeSlotId = timeSlotId,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            });
                        }
                        currentDate = currentDate.AddDays(1);
                    }

                    _context.ShowTimes.AddRange(showTimes);
                }

                // Cập nhật phim
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}