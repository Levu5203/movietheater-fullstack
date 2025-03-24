using System.ComponentModel.DataAnnotations;
using MediatR;
using MovieTheater.Business.ViewModels.auth;

namespace MovieTheater.Business.Handlers.Auth;

public class LoginRequestCommand: IRequest<LoginResponse>
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, ErrorMessage = "{0} must be at least {2} and at max {1} characters long", MinimumLength = 3)]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(20, ErrorMessage = "{0} must be at least {2} and at max {1} characters long", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}