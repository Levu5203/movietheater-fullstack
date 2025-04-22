using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieTheater.Data;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Services;

public class MovieStatusUpdateService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MovieStatusUpdateService> _logger;

    public MovieStatusUpdateService(IServiceScopeFactory scopeFactory, ILogger<MovieStatusUpdateService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Chạy cập nhật ngay khi service bắt đầu (khi chạy project)
        await UpdateMovieStatusesAsync();

        while (!stoppingToken.IsCancellationRequested)
        {
            // Tính thời gian còn lại đến 0h ngày kế tiếp
            var now = DateTime.Now;
            var nextRun = now.Date.AddDays(1); // 0h ngày mai
            var delay = nextRun - now;

            await Task.Delay(delay, stoppingToken);

            // Đến 0h: chạy cập nhật
            await UpdateMovieStatusesAsync();
        }
    }


    private async Task UpdateMovieStatusesAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MovieTheaterDbContext>();

        var today = DateOnly.FromDateTime(DateTime.Today);
        var movies = await dbContext.Movies.ToListAsync();

        foreach (var movie in movies)
        {
            MovieStatus newStatus;

            if (today < movie.ReleasedDate)
                newStatus = MovieStatus.ComingSoon;
            else if (today <= movie.EndDate)
                newStatus = MovieStatus.NowShowing;
            else
                newStatus = MovieStatus.NotAvailable;

            if (movie.Status != newStatus)
            {
                movie.Status = newStatus;
            }
        }

        await dbContext.SaveChangesAsync();
        _logger.LogInformation("Movie statuses updated at {Time}", DateTime.Now);
    }
}

