using System.Collections.Concurrent;

namespace MovieTheater.Business.Manager;

public class ShowtimeQueueManager
{
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _semaphoreByShowtime = new();

    public async Task<T> EnqueueAsync<T>(Guid showtimeId, Func<Task<T>> work)
    {
        var semaphore = _semaphoreByShowtime.GetOrAdd(showtimeId, _ => new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();

        try
        {
            return await work();
        }
        finally
        {
            semaphore.Release();
        }
    }
}
