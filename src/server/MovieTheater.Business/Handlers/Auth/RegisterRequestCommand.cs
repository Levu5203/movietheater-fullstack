using System.ComponentModel.DataAnnotations;
using MediatR;
using MovieTheater.Business.ViewModels.auth;

namespace MovieTheater.Business.Handlers.Auth;

public class RegisterRequestCommand : IRequest<LoginResponse>
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 1)]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 1)]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 1)]
    public required string Username { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public required string Gender { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(18, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 10)]
    public required string IdentityCard { get; set; }

    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 4)]
    public required string Address { get; set; }
}
