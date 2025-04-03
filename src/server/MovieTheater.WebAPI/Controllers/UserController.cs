using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Profile;
using MovieTheater.Business.Handlers.Users;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Core;

namespace MovieTheater.WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        // Lấy Id từ token
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null || !Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("UserId not found or invalid.");
        }

        try
        {
            // Tạo query để lấy profile
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

    [HttpPut("edit")]
    public async Task<IActionResult> EditProfile([FromBody] EditProfileCommand command)
    {
        // Lấy Id từ token
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null || !Guid.TryParse(userId, out var userGuid))
        {
            return BadRequest("UserId not found or invalid.");
        }

        // Gán id vào command
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

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        // Lấy Id từ token
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

