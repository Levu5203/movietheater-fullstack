using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers;
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
    /// Deletes a customer
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
}
