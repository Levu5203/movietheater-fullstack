
using System.ComponentModel.DataAnnotations;
using MovieTheater.Business.ViewModels.Users;
namespace MovieTheater.Business.Handlers.Employees;

public class EmployeeUpdateCommand : MasterBaseUpdateCommand<EmployeeViewModel>
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 1)]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 1)]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    public string Password { get; set; } = "Abc@12345";

    [Required(ErrorMessage = "{0} is required")]
    public required string Gender { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(18, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 10)]
    public required string IdentityCard { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

}
