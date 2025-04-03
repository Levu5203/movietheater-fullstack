namespace MovieTheater.Business.ViewModels.Users;

public class EmployeeViewModel : MasterBaseViewModel
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }
    
    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string DisplayName { get; set; }

    public required string Gender { get; set; }

    public required string IdentityCard { get; set; }

    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }
}
