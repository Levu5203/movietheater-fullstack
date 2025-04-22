using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Profile;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public ChangePasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return false;
        }
        if (request.CurrentPassword == request.NewPassword)
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        return result.Succeeded;
    }
}
