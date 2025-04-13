using System;
using MediatR;
using MovieTheater.Business.Handlers.Invoice;

namespace MovieTheater.WebAPI.Controllers;

public class PendingSeatTimeoutService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30);
    public PendingSeatTimeoutService(
        IServiceProvider serviceProvider,
        ILogger<PendingSeatTimeoutService> logger)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                
                // Gửi command để xử lý các ghế timeout
                var result = await mediator.Send(new CancelExpiredInvoicesCommand(), stoppingToken);
                
                if (result.ProcessedInvoicesCount > 0)
                {
                        System.Console.WriteLine($"Processed {result.ProcessedInvoicesCount} expired invoices, " +
                        $"reset {result.ResetSeatsCount} seats, " +
                        $"removed {result.RemovedTicketsCount} tickets");;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing pending seats timeout", ex);
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }

       
    }
}
