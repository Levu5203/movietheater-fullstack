using System.Globalization;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Models.Common;
using MovieTheater.Models.Security;
using Newtonsoft.Json;

namespace MovieTheater.Data.DataSeeding;

public static class DbInitializer
{
    public static void Seed(MovieTheaterDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager,
        string rolesJsonPath, string usersJsonPath, string roomsJsonPath, string genreJsonPath, string moviesJsonPath, string showTimeSlotsJsonPath, string showTimeJsonPath, string invoicesJsonPath, string historyScoresJsonPath)
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
        var invoices = JsonConvert.DeserializeObject<List<Invoice>>(jsonInvoices);

        string jsonHistoryScores = File.ReadAllText(historyScoresJsonPath);
        var historyScores = JsonConvert.DeserializeObject<List<HistoryScore>>(jsonHistoryScores);

        if (roles == null || users == null || rooms == null || genres == null || movies == null || showTimeSlots == null || showTimes == null || invoices == null || historyScores == null)
        {
            return;
        }

        SeedUserAndRoles(userManager, roleManager, users, roles);
        SeedCinemaRooms(context, rooms);
        SeedGenres(context, genres);
        SeedMovies(context, movies);
        SeedShowTimeSlots(context, showTimeSlots);
        SeedShowTimes(context, showTimes);
        SeedInvoices(context, invoices);
        SeedHistoryScores(context, historyScores);

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
            Password = user.Password,
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
                if (!ExistsInDb<CinemaRoom>(context, r => r.Name == room.Name))
                {
                    context.CinemaRooms.Add(new CinemaRoom
                    {
                        Id = room.Id,
                        Name = room.Name,
                        SeatRows = room.SeatRows,
                        SeatColumns = room.SeatColumns,
                        CreatedAt = DateTime.Now,
                    });
                }
                context.SaveChanges();

                GenerateSeatsForRoom(context, room);
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
            if (!ExistsInDb<Genre>(context, g => g.Type == genre.Type))
            {
                context.Genres.Add(new Genre
                {
                    GenreId = genre.GenreId,
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
            if (!ExistsInDb<Movie>(context, m => m.Name == movie.Name && m.ReleasedDate == movie.ReleasedDate && m.Version == movie.Version))
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
                    CreatedAt = DateTime.Now,
                });
            }
        }
        context.SaveChanges();
    }

    public static void SeedShowTimeSlots(MovieTheaterDbContext context, List<ShowTimeSlot> showTimeSlots)
    {
        foreach (var slot in showTimeSlots)
        {
            if (!ExistsInDb<ShowTimeSlot>(context, s => s.Time == slot.Time))
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
            if (!ExistsInDb<ShowTime>(context, s =>
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


    private static void SeedInvoices(MovieTheaterDbContext context, List<Invoice> invoices)
    {
        if (ExistsInDb<Invoice>(context, x => true))
        {
            return;
        }

        foreach (var invoice in invoices)
        {
            if (!ExistsInDb<Invoice>(context, x => x.Id == invoice.Id))
            {
                context.Invoices.Add(invoice);
            }
        }
        context.SaveChanges();
    }

    private static void SeedHistoryScores(MovieTheaterDbContext context, List<HistoryScore> historyScores)
    {
        if (ExistsInDb<HistoryScore>(context, x => true))
        {
            return;
        }

        foreach (var historyScore in historyScores)
        {
            if (!ExistsInDb<HistoryScore>(context, x => x.Id == historyScore.Id))
            {
                context.HistoryScores.Add(historyScore);
            }
        }
        context.SaveChanges();
    }

    private static bool ExistsInDb<T>(MovieTheaterDbContext context, Func<T, bool> predicate) where T : class
    {
        return context.Set<T>().Any(predicate);
    }
}

internal class UserJsonViewModel
{
    public Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string UserName { get; set; }

    public required string Address { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string IdentityCard { get; set; }

    public required string Gender { get; set; }

    public required int TotalScore { get; set; }

    public required string PhoneNumber { get; set; }

    public required string DateOfBirth { get; set; }

    public required string Role { get; set; }
}

public class ShowTimeJsonViewModel
{
    public required Guid ShowTimeId { get; set; }
    public required Guid MovieId { get; set; }
    public required Guid CinemaRoomId { get; set; }
    public required Guid ShowTimeSlotId { get; set; }
    public required string ShowDate { get; set; }
}