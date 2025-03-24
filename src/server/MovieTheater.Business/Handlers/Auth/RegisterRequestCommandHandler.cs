using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MovieTheater.Business.Services;
using MovieTheater.Business.ViewModels.auth;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;
using static MovieTheater.Core.Constants.CoreConstants;

namespace MovieTheater.Business.Handlers.Auth;

public class RegisterRequestCommandHandler : BaseHandler, IRequestHandler<RegisterRequestCommand, LoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public RegisterRequestCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        UserManager<User> userManager, 
        ITokenService tokenService,
        IConfiguration configuration) : base(unitOfWork, mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Handle(RegisterRequestCommand request, CancellationToken cancellationToken)
    {
        // Check if user with this username or email already exists
        var existingUserByName = await _userManager.FindByNameAsync(request.Username);
        if (existingUserByName != null)
        {
            throw new InvalidOperationException("Username already exists");
        }
        
        var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingUserByEmail != null)
        {
            throw new InvalidOperationException("Email already exists");
        }
        
        // Create new user
        var user = new User
        {
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            IdentityCard = request.IdentityCard,
            TotalScore = 0,
            Password = request.Password,
            Address = request.Address,
            IsActive = true,
            EmailConfirmed = true 
        };
        
        // Add the user using UserManager
        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new InvalidOperationException($"Failed to create user: {string.Join(", ", errors)}");
        }
        
        // Assign default role
        await _userManager.AddToRoleAsync(user, RoleConstants.Customer);
        
        // Get user roles
        var userRoles = await _userManager.GetRolesAsync(user);
        
        // Generate access token
        var accessToken = await _tokenService.GenerateTokenAsync(user, userRoles);
        
        // Generate refresh token
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id);
        
        // Get token expiration from config
        if (!int.TryParse(_configuration["JWT:AccessTokenExpiryMinutes"], out var expiryMinutes))
        {
            expiryMinutes = 15;
        }
        
        // Return login response
        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes)
        };
    }
}
