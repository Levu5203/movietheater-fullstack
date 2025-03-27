using MediatR;

namespace MovieTheater.Business.Handlers.Auth;

public class ResetPasswordCommand : IRequest<bool>
{
    public required string Email { get; set; }

    public required string Token { get; set; }

    public required string NewPassword { get; set; }
}
