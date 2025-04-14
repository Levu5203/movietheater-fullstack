using System.Globalization;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Models.Common;
using MovieTheater.Models.Security;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace MovieTheater.Data.DataSeeding;

public static class DbInitializer
{
    public static void Seed(MovieTheaterDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager,
        string rolesJsonPath, string usersJsonPath, string roomsJsonPath, string genreJsonPath, string moviesJsonPath,
        string showTimeSlotsJsonPath, string showTimeJsonPath, string invoicesJsonPath, string historyScoresJsonPath,
        string promotionsJsonPath)
    {
        context.Database.EnsureCreated();

        string jsonRoles = File.ReadAllText(rolesJsonPath);
        var roles = JsonConvert.DeserializeObject<List<Role>>(jsonRoles);

        string jsonUsers = File.ReadAllText(usersJsonPath);
        var users = JsonConvert.DeserializeObject<List<UserJsonViewModel>>(jsonUsers);

        string jsonRooms = File.ReadAllText(roomsJsonPath);
        var rooms = JsonConvert.DeserializeObject<List<CinemaRoom>>(jsonRooms);

        string jsonGenre = File.ReadAllText(genreJsonPath);
        var genres = JsonConvert.DeserializeObject<List<Genre>>(jsonGenre);

        string jsonMovies = File.ReadAllText(moviesJsonPath);
        var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonMovies);

        string jsonShowTimeSlots = File.ReadAllText(showTimeSlotsJsonPath);
        var showTimeSlots = JsonConvert.DeserializeObject<List<ShowTimeSlot>>(jsonShowTimeSlots);

        string jsonShowTimes = File.ReadAllText(showTimeJsonPath);
        var showTimes = JsonConvert.DeserializeObject<List<ShowTimeJsonViewModel>>(jsonShowTimes);

        string jsonInvoices = File.ReadAllText(invoicesJsonPath);
        var invoices = JsonConvert.DeserializeObject<List<InvoiceJsonViewModel>>(jsonInvoices);

        string jsonHistoryScores = File.ReadAllText(historyScoresJsonPath);
        var historyScores = JsonConvert.DeserializeObject<List<HistoryScoreJsonViewModel>>(jsonHistoryScores);

        string jsonPromotions = File.ReadAllText(promotionsJsonPath);
        var promotions = JsonConvert.DeserializeObject<List<Promotion>>(jsonPromotions);


        if (roles == null || users == null || rooms == null || genres == null || movies == null ||
            showTimeSlots == null || showTimes == null || invoices == null || historyScores == null ||
            promotions == null)
        {
            return;
        }

        SeedUserAndRoles(userManager, roleManager, users, roles);
        SeedCinemaRooms(context, rooms);
        SeedGenres(context, genres);
        SeedMovies(context, movies);
        SeedMovieGenres(context);
        SeedShowTimeSlots(context, showTimeSlots);
        SeedShowTimes(context, showTimes);
        SeedInvoices(context, invoices);
        SeedHistoryScores(context, historyScores);
        SeedPromotions(context, promotions);
        SeedTickets(context);

        context.SaveChanges();
    }

    private static void SeedUserAndRoles(UserManager<User> userManager, RoleManager<Role> roleManager, List<UserJsonViewModel> users, List<Role> roles)
    {
        if (userManager.Users.Any(x => x.UserName == "systemadministrator") || users == null)
        {
            return;
        }

        var passwordHash = new PasswordHasher<User>();

        foreach (var user in users)
        {
            var newUser = CreateUser(user, passwordHash, userManager);
            if (newUser == null)
            {
                continue;
            }

            var result = userManager.CreateAsync(newUser, user.Password).Result;
            if (!result.Succeeded)
            {
                continue;
            }

            EnsureRoleExists(roleManager, roles, user.Role, userManager);
            userManager.AddToRoleAsync(newUser, user.Role).Wait();
        }
    }

    private static User? CreateUser(UserJsonViewModel user, PasswordHasher<User> passwordHash, UserManager<User> userManager)
    {
        var newUser = new User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Address = user.Address,
            Gender = user.Gender,
            IdentityCard = user.IdentityCard,
            TotalScore = user.TotalScore,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = DateTime.ParseExact(
                user.DateOfBirth,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture
            ).ToLocalTime(),
            EmailConfirmed = true,
            IsActive = true,
        };

        newUser.PasswordHash = passwordHash.HashPassword(newUser, user.Password);

        var systemAdministrator = userManager.FindByNameAsync("systemadministrator").Result;
        if (systemAdministrator != null)
        {
            newUser.CreatedById = systemAdministrator.Id;
        }

        return newUser;
    }

    private static void EnsureRoleExists(RoleManager<Role> roleManager, List<Role> roles, string roleName, UserManager<User> userManager)
    {
        var userRole = roleManager.FindByNameAsync(roleName).Result;
        if (userRole != null)
        {
            return;
        }

        var newRole = roles.FirstOrDefault(x => x.Name == roleName);
        if (newRole == null)
        {
            return;
        }

        var systemAdministrator = userManager.FindByNameAsync("systemadministrator").Result;
        if (systemAdministrator != null)
        {
            newRole.CreatedById = systemAdministrator.Id;
        }

        roleManager.CreateAsync(newRole).Wait();
    }

    private static void SeedCinemaRooms(MovieTheaterDbContext context, List<CinemaRoom> cinemaRooms)
    {
        if (cinemaRooms != null)
        {
            foreach (var room in cinemaRooms)
            {
                if (!ExistsInDb<CinemaRoom>(context, r => r.Id == room.Id || r.Name == room.Name))
                {
                    context.CinemaRooms.Add(new CinemaRoom
                    {
                        Id = room.Id,
                        Name = room.Name,
                        SeatRows = room.SeatRows,
                        SeatColumns = room.SeatColumns,
                        CreatedAt = DateTime.Now,
                    });
                    GenerateSeatsForRoom(context, room);
                }
                context.SaveChanges();
            }
        }
    }

    private static void GenerateSeatsForRoom(MovieTheaterDbContext context, CinemaRoom room)
    {
        var seats = new List<Seat>();

        for (int row = 1; row <= room.SeatRows; row++)
        {
            char rowChar = (char)('A' + row - 1);

            for (int column = 1; column <= room.SeatColumns; column++)
            {
                var seat = new Seat
                {
                    Row = rowChar,
                    Column = column,
                    SeatType = SeatType.STANDARD,
                    CinemaRoomId = room.Id,
                    CreatedAt = DateTime.Now,
                    seatStatus = SeatStatus.Available
                };

                seats.Add(seat);
            }
        }

        context.Seats.AddRange(seats);
        context.SaveChanges();
    }
    public static void SeedGenres(MovieTheaterDbContext context, List<Genre> genres)
    {
        foreach (var genre in genres)
        {
            if (!ExistsInDb<Genre>(context, g => g.Id == genre.Id || g.Type == genre.Type))
            {
                context.Genres.Add(new Genre
                {
                    Id = genre.Id,
                    Type = genre.Type
                });
            }
        }

        context.SaveChanges();
    }

    public static void SeedMovies(MovieTheaterDbContext context, List<Movie> movies)
    {
        foreach (var movie in movies)
        {
            if (!ExistsInDb<Movie>(context, m => m.Name == movie.Name && m.ReleasedDate == movie.ReleasedDate && m.Version == movie.Version || m.Id == movie.Id))
            {
                context.Movies.Add(new Movie
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Duration = movie.Duration,
                    Origin = movie.Origin,
                    Description = movie.Description,
                    Version = movie.Version,
                    PosterUrl = movie.PosterUrl,
                    Status = movie.Status,
                    ReleasedDate = movie.ReleasedDate,
                    EndDate = movie.EndDate,
                    Actors = movie.Actors,
                    Director = movie.Director,
                    CreatedAt = DateTime.Now,
                });
            }
        }
        context.SaveChanges();
    }

    private static void SeedMovieGenres(MovieTheaterDbContext context)
    {
        var moviesWithoutGenres = context.Movies
            .Where(m => !context.MovieGenres.Any(mg => mg.MovieId == m.Id))
            .ToList();

        var genres = context.Genres.ToList();
        var random = new Random();

        foreach (var movie in moviesWithoutGenres)
        {
            // Chọn ngẫu nhiên 1-3 thể loại cho mỗi phim
            int numberOfGenres = random.Next(1, 4);
            var selectedGenres = genres.OrderBy(x => random.Next()).Take(numberOfGenres).ToList();

            foreach (var genre in selectedGenres)
            {
                var movieGenre = new MovieGenre
                {
                    Id = Guid.NewGuid(),
                    MovieId = movie.Id,
                    GenreId = genre.Id,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };

                context.MovieGenres.Add(movieGenre);
            }
        }

        context.SaveChanges();
    }


    public static void SeedShowTimeSlots(MovieTheaterDbContext context, List<ShowTimeSlot> showTimeSlots)
    {
        foreach (var slot in showTimeSlots)
        {
            if (!ExistsInDb<ShowTimeSlot>(context, s => s.ShowTimeSlotId == slot.ShowTimeSlotId || s.Time == slot.Time))
            {
                context.ShowTimeSlots.Add(slot);
            }
        }
        context.SaveChanges();
    }

    public static void SeedShowTimes(MovieTheaterDbContext context, List<ShowTimeJsonViewModel> showTimes)
    {
        foreach (var showTime in showTimes)
        {
            if (!ExistsInDb<ShowTime>(context, s => s.Id == showTime.ShowTimeId ||
                s.CinemaRoomId == showTime.CinemaRoomId &&
                s.ShowTimeSlotId == showTime.ShowTimeSlotId &&
                s.ShowDate == DateOnly.Parse(showTime.ShowDate)))
            {
                var movie = context.Movies.Find(showTime.MovieId);
                var timeSlot = context.ShowTimeSlots.Find(showTime.ShowTimeSlotId);

                if (movie == null || timeSlot == null)
                {
                    Console.WriteLine($"Warning: Movie or TimeSlot not found for ShowTime {showTime.ShowTimeId}");
                    continue;
                }

                var showDate = DateOnly.Parse(showTime.ShowDate);
                if (HasTimeConflict(context, showTime.CinemaRoomId, showDate, timeSlot.Time, movie.Duration, showTime.ShowTimeId))
                {
                    Console.WriteLine($"Warning: Time conflict detected for ShowTime {showTime.ShowTimeId} in room {showTime.CinemaRoomId} at {showDate} {timeSlot.Time}");
                    continue;
                }

                context.ShowTimes.Add(new ShowTime
                {
                    Id = showTime.ShowTimeId,
                    MovieId = showTime.MovieId,
                    CinemaRoomId = showTime.CinemaRoomId,
                    ShowTimeSlotId = showTime.ShowTimeSlotId,
                    ShowDate = showDate,
                    CreatedAt = DateTime.Now,
                });
            }
        }
        context.SaveChanges();
    }

    private static bool HasTimeConflict(MovieTheaterDbContext context, Guid roomId, DateOnly showDate, TimeSpan startTime, int duration, Guid currentShowTimeId)
    {
        var showTimesInRoom = context.ShowTimes
            .Where(s => s.CinemaRoomId == roomId && s.ShowDate == showDate && s.Id != currentShowTimeId)
            .Select(s => new
            {
                s.ShowTimeSlot!.Time,
                s.Movie!.Duration
            })
            .ToList();

        var newEndTime = startTime.Add(TimeSpan.FromMinutes(duration));

        foreach (var existingShowTime in showTimesInRoom)
        {
            var existingEndTime = existingShowTime.Time.Add(TimeSpan.FromMinutes(existingShowTime.Duration));

            if ((startTime >= existingShowTime.Time && startTime < existingEndTime) ||
                (newEndTime > existingShowTime.Time && newEndTime <= existingEndTime) ||
                (startTime <= existingShowTime.Time && newEndTime >= existingEndTime))
            {
                return true;
            }
        }

        return false;
    }


    private static void SeedInvoices(MovieTheaterDbContext context, List<InvoiceJsonViewModel> invoices)
    {
        if (ExistsInDb<Invoice>(context, x => true))
        {
            return;
        }

        foreach (var invoice in invoices)
        {
            if (!ExistsInDb<Invoice>(context, x => x.Id == invoice.Id))
            {
                var user = context.Users.Find(invoice.UserId);
                var showTime = context.ShowTimes.Find(invoice.ShowTimeId);
                var cinemaRoom = context.CinemaRooms.Find(invoice.CinemaRoomId);
                var movie = context.Movies.Find(invoice.MovieId);


                if (user == null || showTime == null)
                {
                    Console.WriteLine($"Warning: User or ShowTime not found for Invoice {invoice.Id}");
                    continue;
                }

                context.Invoices.Add(new Invoice
                {
                    Id = invoice.Id,
                    TotalMoney = invoice.TotalMoney,
                    AddedScore = invoice.AddedScore,
                    UserId = invoice.UserId,
                    User = user,
                    ShowTimeId = invoice.ShowTimeId,
                    ShowTime = showTime,
                    CinemaRoomId = invoice.CinemaRoomId,
                    CinemaRoom = cinemaRoom,
                    MovieId = invoice.MovieId,
                    Movie = movie,
                    CreatedAt = invoice.CreatedAt,
                    CreatedById = invoice.CreatedById,
                    IsActive = invoice.IsActive,
                    IsDeleted = invoice.IsDeleted,
                    InvoiceStatus = InvoiceStatus.Paid
                });
            }
        }
        context.SaveChanges();
    }

    private static void SeedHistoryScores(MovieTheaterDbContext context, List<HistoryScoreJsonViewModel> historyScores)
    {
        if (ExistsInDb<HistoryScore>(context, x => true))
        {
            return;
        }

        foreach (var historyScore in historyScores)
        {
            if (!ExistsInDb<HistoryScore>(context, x => x.Id == historyScore.Id))
            {
                var invoice = context.Invoices.Find(historyScore.InvoiceId);

                if (invoice == null)
                {
                    Console.WriteLine($"Warning: Invoice not found for HistoryScore {historyScore.Id}");
                    continue;
                }

                context.HistoryScores.Add(new HistoryScore
                {
                    Id = historyScore.Id,
                    Score = historyScore.Score,
                    ScoreStatus = (ScoreStatus)historyScore.ScoreStatus,
                    Description = historyScore.Description,
                    InvoiceId = historyScore.InvoiceId,
                    Invoice = invoice,
                    CreatedAt = historyScore.CreatedAt
                });
            }
        }
        context.SaveChanges();
    }

    private static void SeedPromotions(MovieTheaterDbContext context, List<Promotion> promotions)
    {
        foreach (var promotion in promotions)
        {
            if (!ExistsInDb<Promotion>(context, x => x.Id == promotion.Id))
            {
                context.Promotions.Add(promotion);
            }
        }
        context.SaveChanges();
    }

    private static void SeedTickets(MovieTheaterDbContext context)
    {
        var invoices = context.Invoices
            .Where(i => i.Tickets.Count == 0)
            .Include(i => i.ShowTime)
            .Include(i => i.CinemaRoom)
            .Include(i => i.Movie)
            .ToList();

        var random = new Random();

        foreach (var invoice in invoices)
        {
            var seats = context.Seats
                .Where(s => s.CinemaRoomId == invoice.CinemaRoomId)
                .ToList();

            int numberOfTickets = random.Next(1, 4);

            var selectedSeats = seats.OrderBy(x => random.Next()).Take(numberOfTickets).ToList();

            // Tạo vé cho mỗi ghế đã chọn
            foreach (var seat in selectedSeats)
            {
                var ticket = new Ticket
                {
                    Id = Guid.NewGuid(),
                    Price = 100000, // Giá vé mặc định
                    BookingDate = DateTime.Now,
                    Status = TicketStatus.Paid,
                    InvoiceId = invoice.Id,
                    SeatId = seat.Id,
                    Seat = seat,
                    CinemaRoomId = invoice.CinemaRoomId,
                    CinemaRoom = invoice.CinemaRoom,
                    ShowTimeId = invoice.ShowTimeId,
                    ShowTime = invoice.ShowTime,
                    MovieId = invoice.MovieId,
                    Movie = invoice.Movie,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false
                };

                context.Tickets.Add(ticket);
            }
        }

        context.SaveChanges();
    }

    private static bool ExistsInDb<T>(MovieTheaterDbContext context, Func<T, bool> predicate) where T : class
    {
        return context.Set<T>().Any(predicate);
    }
}
