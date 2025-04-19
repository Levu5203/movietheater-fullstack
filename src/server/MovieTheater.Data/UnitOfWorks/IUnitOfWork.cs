using Microsoft.EntityFrameworkCore.Storage;
using MovieTheater.Data.Repositories;
using MovieTheater.Models;
using MovieTheater.Models.Common;
using MovieTheater.Models.Security;

namespace MovieTheater.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    MovieTheaterDbContext Context { get; }

    #region Master Data Repositories
    IMasterDataRepository<User> UserRepository { get; }

    IMasterDataRepository<Role> RoleRepository { get; }

    IMasterDataRepository<Seat> SeatRepository { get; }

    IMasterDataRepository<CinemaRoom> CinemaRoomRepository { get; }

    IMasterDataRepository<Movie> MovieRepository { get; }

    IMasterDataRepository<Invoice> InvoiceRepository { get; }

    IMasterDataRepository<ShowTime> ShowtimeRepository { get; }

    IMasterDataRepository<Promotion> PromotionRepository { get; }

    IMasterDataRepository<Ticket> TicketRepository { get; }

    IRepository<ShowTimeSlot> ShowtimeSlotRepository { get; }
    
    IRepository<HistoryScore> HistoryScoreRepository { get; }

    IRepository<RefreshToken> RefreshTokenRepository { get; }

    IRepository<Genre> GenreRepository { get; }

    IRepository<MovieGenre> MovieGenreRepository { get; }
    IRepository<SeatShowTime> SeatShowtimeRepository { get; }

    #endregion

    #region Repositories

    IRepository<T> Repository<T>() where T : BaseEntity, IBaseEntity;

    #endregion

    int SaveChanges();

    Task<int> SaveChangesAsync();

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}