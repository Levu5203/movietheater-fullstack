using Microsoft.EntityFrameworkCore.Storage;
using MovieTheater.Data.Repositories;
using MovieTheater.Models;
using MovieTheater.Models.Common;
using MovieTheater.Models.Security;

namespace MovieTheater.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly MovieTheaterDbContext _context;

    private readonly IUserIdentity _currentUser;

    private bool _disposed = false;

    public UnitOfWork(MovieTheaterDbContext context, IUserIdentity currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public MovieTheaterDbContext Context => _context;

    #region Implementation of Master Data Repositories
    private IMasterDataRepository<User>? _userRepository;
    public IMasterDataRepository<User> UserRepository => _userRepository ??= new MasterDataRepository<User>(_context, _currentUser);

    private IMasterDataRepository<Role>? _roleRepository;
    public IMasterDataRepository<Role> RoleRepository => _roleRepository ??= new MasterDataRepository<Role>(_context, _currentUser);

    private IMasterDataRepository<Seat>? _seatRepository;
    public IMasterDataRepository<Seat> SeatRepository => _seatRepository ??= new MasterDataRepository<Seat>(_context, _currentUser);

    private IMasterDataRepository<CinemaRoom>? _cinemaRoomRepository;
    public IMasterDataRepository<CinemaRoom> CinemaRoomRepository => _cinemaRoomRepository ??= new MasterDataRepository<CinemaRoom>(_context, _currentUser);

    private IMasterDataRepository<Movie>? _movieRepository;
    public IMasterDataRepository<Movie> MovieRepository => _movieRepository ??= new MasterDataRepository<Movie>(_context, _currentUser);

    private IMasterDataRepository<Invoice>? _invoiceRepository;
    public IMasterDataRepository<Invoice> InvoiceRepository => _invoiceRepository ??= new MasterDataRepository<Invoice>(_context, _currentUser);

    private IMasterDataRepository<ShowTime>? _showTimeRepository;

    public IMasterDataRepository<ShowTime> ShowtimeRepository => _showTimeRepository ??= new MasterDataRepository<ShowTime>(_context, _currentUser);

    private IMasterDataRepository<Promotion>? _promotionRepository;

    public IMasterDataRepository<Promotion> PromotionRepository => _promotionRepository ??= new MasterDataRepository<Promotion>(_context, _currentUser);

    private IMasterDataRepository<Ticket>? _ticketRepository;

    public IMasterDataRepository<Ticket> TicketRepository => _ticketRepository ??= new MasterDataRepository<Ticket>(_context, _currentUser);

    private IRepository<SeatShowTime>? _seatShowTimeRepository;
    public IRepository<SeatShowTime> SeatShowTimeRepository => _seatShowTimeRepository ??= new Repository<SeatShowTime>(_context, _currentUser);

    private IRepository<HistoryScore>? _HistoryScoreRepository;
    public IRepository<HistoryScore> HistoryScoreRepository => _HistoryScoreRepository ??= new Repository<HistoryScore>(_context, _currentUser);

    private IRepository<TicketShowtimeMovie>? _ticketShowtimeMovieRepository;
    public IRepository<TicketShowtimeMovie> TicketShowtimeMovieRepository => _ticketShowtimeMovieRepository ??= new Repository<TicketShowtimeMovie>(_context, _currentUser);


    #endregion

    #region Implementation of Repositories

    private IRepository<RefreshToken>? _refreshTokenRepository;

    public IRepository<RefreshToken> RefreshTokenRepository => _refreshTokenRepository ??= new Repository<RefreshToken>(_context, _currentUser);

    public IRepository<T> Repository<T>() where T : BaseEntity, IBaseEntity
    {
        return new Repository<T>(_context, _currentUser);
    }

    #endregion
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}