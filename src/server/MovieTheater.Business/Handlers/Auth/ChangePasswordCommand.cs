using MediatR;

namespace MovieTheater.Business.Handlers.Auth;

public class ChangePasswordCommand : IRequest<bool>
{
    public required Guid UserId { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
    public required string ConfirmPassword { get; set; }
}
