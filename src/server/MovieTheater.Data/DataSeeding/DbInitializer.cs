using Microsoft.AspNetCore.Identity;
using MovieTheater.Models.Security;
using Newtonsoft.Json;

namespace MovieTheater.Data.DataSeeding;

public static class DbInitializer
{
    public static void Seed(MovieTheaterDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager,
        string rolesJsonPath, string usersJsonPath)
    {
        context.Database.EnsureCreated();

        string jsonRoles = File.ReadAllText(rolesJsonPath);
        var roles = JsonConvert.DeserializeObject<List<Role>>(jsonRoles);

        string jsonUsers = File.ReadAllText(usersJsonPath);
        var users = JsonConvert.DeserializeObject<List<UserJsonViewModel>>(jsonUsers);

        if (roles == null || users == null)
        {
            return;
        }

        SeedUserAndRoles(userManager, roleManager, users, roles);

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

    public required string Role { get; set; }
}