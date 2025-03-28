using MediatR;

namespace MovieTheater.Business.Handlers.Auth;

public class ForgotPasswordCommand : IRequest<bool>
{
    public required string Email { get; set; }
}
