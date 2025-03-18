using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Core.Constants;
using MovieTheater.Models.Common;
using MovieTheater.Models.Security;

namespace MovieTheater.Data;

public class MovieTheaterDbContext(DbContextOptions<MovieTheaterDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<ShowTime> ShowTimes { get; set; }
    public DbSet<ShowTimeSlot> ShowTimeSlots { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<CinemaRoom> CinemaRooms { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<SeatShowTime> SeatShowTimes { get; set; }
    public DbSet<HistoryScore> HistoryScores { get; set; }
    public DbSet<TicketShowtimeMovie> TicketShowtimeMovies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users", CoreConstants.Schemas.Security);
        builder.Entity<Role>().ToTable("Roles", CoreConstants.Schemas.Security);
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", CoreConstants.Schemas.Security);
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", CoreConstants.Schemas.Security);
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", CoreConstants.Schemas.Security);
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", CoreConstants.Schemas.Security);
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", CoreConstants.Schemas.Security);

        // Relationship between User&Invoice
        builder.Entity<Invoice>()
            .HasOne(i => i.User) // 1 invoice belongs to 1 user
            .WithMany() // 1 user has many invoices
            .HasForeignKey(i => i.UserId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete
        
        // Relationship between Invoice&SHistoryScore
        builder.Entity<HistoryScore>()
            .HasOne(h => h.Invoice) // 1 history score belongs to 1 invoice
            .WithMany(i => i.HistoryScores) // 1 invoice has many history scores
            .HasForeignKey(h => h.InvoiceId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete

        // Relationship between Invoice&Ticket
        builder.Entity<Ticket>()
            .HasOne(t => t.Invoice) // 1 ticket belongs to 1 invoice
            .WithMany(i => i.Tickets) // 1 invoice has many tickets
            .HasForeignKey(t => t.InvoiceId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete

        // Relationship between Invoice&ShowTime
        builder.Entity<Invoice>()
            .HasOne(i => i.ShowTime) // 1 invoice belongs to 1 show time
            .WithMany(s => s.Invoices) // 1 show time has many invoices
            .HasForeignKey(i => i.ShowTimeId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete
        
        // Relationship between Ticket&Seat
        builder.Entity<Ticket>()
            .HasOne(t => t.Seat) // 1 ticket belongs to 1 seat
            .WithMany(s => s.Tickets) // 1 seat has many tickets
            .HasForeignKey(t => t.SeatId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete
        
        // Relationship between Ticket&Promotion
        builder.Entity<Ticket>()
            .HasOne(t => t.Promotion) // 1 ticket belongs to 1 promotion
            .WithMany(p => p.Tickets) // 1 promotion has many tickets
            .HasForeignKey(t => t.PromotionId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete
        
        // Relationship between Seat&CinemaRoom
        builder.Entity<Seat>()
            .HasOne(s => s.CinemaRoom) // 1 seat belongs to 1 cinema room
            .WithMany(c => c.Seats) // 1 cinema room has many seats
            .HasForeignKey(s => s.CinemaRoomId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete

        // Relationship between CinemaRoom&ShowTime
        builder.Entity<ShowTime>()
            .HasOne(s => s.CinemaRoom) // 1 show time belongs to 1 cinema room
            .WithMany(c => c.ShowTimes) // 1 cinema room has many show times
            .HasForeignKey(s => s.CinemaRoomId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete

        // Relationship between ShowTime&Movie
        builder.Entity<ShowTime>()
            .HasOne(s => s.Movie) // 1 show time belongs to 1 movie
            .WithMany(m => m.ShowTimes) // 1 movie has many show times
            .HasForeignKey(s => s.MovieId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete

        // Relationship between ShowTime&ShowTimeSlot
        builder.Entity<ShowTime>()
            .HasOne(s => s.ShowTimeSlot) // 1 show time belongs to 1 show time slot
            .WithMany(st => st.ShowTimes) // 1 show time slot has many show times
            .HasForeignKey(s => s.ShowTimeSlotId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete
        
        // Set up Movie&MovieGenre composite key
        builder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId }); // Composite key
        
        // relationship between Movie&MovieGenre
        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie) // 1 movie genre belongs to 1 movie
            .WithMany(m => m.MovieGenres) // 1 movie has many movie genres
            .HasForeignKey(mg => mg.MovieId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete
        
        // Relationship between Genre&MovieGenre
        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre) // 1 movie genre belongs to 1 genre
            .WithMany(g => g.MovieGenres) // 1 genre has many movie genres
            .HasForeignKey(mg => mg.GenreId) // Foreign key
            .OnDelete(DeleteBehavior.NoAction); // No action when delete
        
        // Global query filter for soft delete
        builder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Role>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Invoice>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ShowTime>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Ticket>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<CinemaRoom>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Movie>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Promotion>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Seat>().HasQueryFilter(x => !x.IsDeleted);
    }
}
