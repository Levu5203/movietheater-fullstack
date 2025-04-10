using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Profile;

namespace MovieTheater.WebAPI.Controllers;

/// <summary>
/// Controller for managing user-related operations.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Retrieves the profile information of the currently logged-in user.
    /// </summary>
    /// <returns>The user profile data if found; otherwise, appropriate error response.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null || !Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("UserId not found or invalid.");
        }

        try
        {
            var query = new ProfileGetByIdQuery { Id = userGuid };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound("User not found.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates the profile information of the currently logged-in user.
    /// </summary>
    /// <param name="command">The profile data to be updated.</param>
    /// <returns>Updated user profile information or error response.</returns>
    [HttpPut("edit")]
    public async Task<IActionResult> EditProfile([FromForm] EditProfileCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null || !Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("UserId not found or invalid.");
        }

        command.Id = userGuid;

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Controller: {ex.Message}");
        }
    }

    /// <summary>
    /// Changes the password of the currently logged-in user.
    /// </summary>
    /// <param name="command">The password change request including current and new passwords.</param>
    /// <returns>Result of password change operation.</returns>
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null || !Guid.TryParse(userId, out _))
        {
            return BadRequest("UserId not found or invalid.");
        }

        if (command.CurrentPassword == command.NewPassword)
        {
            return BadRequest(new { message = "New password must be different from current password!" });
        }

        var result = await _mediator.Send(command);

        if (!result)
        {
            return BadRequest(new
            {
                message = "Error changing password! Try again."
            });
        }

        return Ok(new { message = "Password has been changed successfully." });
    }
}
