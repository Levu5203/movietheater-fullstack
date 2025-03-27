using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MovieTheater.Business.Services;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Auth;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
{
    private readonly UserManager<User> _userManager;

    private readonly IEmailService _emailService;

        private readonly IConfiguration _configuration;


    public ForgotPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService,  IConfiguration configuration)
    {
        _userManager = userManager;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<bool> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null) return false;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Build the reset link manually
        var frontendBaseUrl = _configuration["FrontendSettings:BaseUrl"];
        var resetLink = $"{frontendBaseUrl}/reset-password?email={user.Email}&token={Uri.UnescapeDataString(token)}";

        var emailBody = $"Click <a href='{resetLink}'>here</a> to reset your password. The link is valid for 1 hour.";

        await _emailService.SendEmailAsync(user.Email!, "Reset Password", emailBody);

        return true;
    }
}