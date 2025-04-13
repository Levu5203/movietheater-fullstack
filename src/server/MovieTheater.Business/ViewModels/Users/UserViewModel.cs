namespace MovieTheater.Business.ViewModels.Users;

public class UserViewModel : MasterBaseViewModel
{
    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string DisplayName { get; set; }

    public required string Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Avatar { get; set; }
}
