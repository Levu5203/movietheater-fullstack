using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MovieTheater.Business.Handlers.Movie;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Handlers;

public class UpdateMovieQueryHandler : IRequestHandler<UpdateMovieQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UpdateMovieQueryHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> Handle(UpdateMovieQuery request, CancellationToken cancellationToken)
    {
        var movie = await _unitOfWork.MovieRepository.GetByIdAsync(request.Id);
        if (movie == null)
        {
            return false;
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

        // Upload new image if provided
        if (request.PosterImage != null)
        {
            // Xoá ảnh cũ nếu có
            if (!string.IsNullOrEmpty(movie.PosterUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, movie.PosterUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            // Upload ảnh mới
            var posterUrl = await UploadPosterImage(request.PosterImage);
            movie.PosterUrl = posterUrl;
        }

        // Remove old genres & add new ones
        var oldGenres = _unitOfWork.MovieGenreRepository
            .GetQuery(mg => mg.MovieId == movie.Id)
            .ToList();

        // Tạo danh sách genreId mới cần gán
        var newGenreTypes = request.SelectedGenres
            ?.Select(g => Enum.Parse<GenreType>(g))
            .ToList() ?? new List<GenreType>();

        var allGenres = _unitOfWork.GenreRepository.GetQuery().ToList();
        var newGenreIds = allGenres
            .Where(g => newGenreTypes.Contains(g.Type))
            .Select(g => g.Id)
            .ToList();

        // 1. Cập nhật genre cũ: nếu không nằm trong danh sách mới => IsDeleted = true
        foreach (var old in oldGenres)
        {
            if (!newGenreIds.Contains(old.GenreId))
            {
                old.IsDeleted = true;
            }
            else
            {
                old.IsDeleted = false; // nếu trước đó là true thì khôi phục
            }
        }

        // 2. Thêm mới các genre chưa có
        var oldGenreIds = oldGenres.Select(x => x.GenreId).ToHashSet();
        var genresToAdd = newGenreIds.Where(id => !oldGenreIds.Contains(id)).ToList();

        foreach (var genreId in genresToAdd)
        {
            await _unitOfWork.MovieGenreRepository.AddAsync(new MovieGenre
            {
                MovieId = movie.Id,
                GenreId = genreId
            });
        }
        

        // Remove old showtimes
        var oldShowTimes = _unitOfWork.ShowtimeRepository.GetQuery(s => s.MovieId == movie.Id).ToList();
        _unitOfWork.ShowtimeRepository.Delete(oldShowTimes);

        // Create new showtimes
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

            await _unitOfWork.ShowtimeRepository.AddAsync(showTimes);
        }

        //  Update Movie
        _unitOfWork.MovieRepository.Update(movie);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    private async Task<string> UploadPosterImage(IFormFile posterImage)
    {
        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/posters");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(posterImage.FileName)}";
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await posterImage.CopyToAsync(fileStream);
        }

        return $"/uploads/posters/{uniqueFileName}";
    }
}
