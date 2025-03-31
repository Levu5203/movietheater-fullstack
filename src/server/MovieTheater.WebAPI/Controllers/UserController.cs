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

    /// <summary>
    /// Get list of customers
    /// </summary>
    /// <returns>List of customers</returns>
    [HttpGet("customers")]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCustomerAsync()
    {
        var users = new UserGetAllQuery();
        var result = await _mediator.Send(users);
        return Ok(result);
    }

    /// <summary>
    /// Search for customers
    /// </summary>
    /// <returns>List of customers match to keyword and filters</returns>
    [HttpPost("customers/search")]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(PaginatedResult<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchCustomerAsync([FromBody] UserSearchCustomerQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id"></param>
    /// <returns>boolean</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new UserDeleteByIdCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }


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
            var query = new GetProfileByIdQuery { Id = userGuid };

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

