using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Auth;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool> {

    private readonly UserManager<User> _userManager;

    public ResetPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return false;
        }
        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        return result.Succeeded;
    }
}
