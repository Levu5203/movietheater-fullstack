using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Customers;
using MovieTheater.Business.Handlers.Users;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Core;
using MovieTheater.Models.Security;

namespace MovieTheater.WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class CustomersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Get list of customers
    /// </summary>
    /// <returns>List of customers</returns>
    [HttpGet()]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCustomerAsync()
    {
        var users = new CustomerGetAllQuery();
        var result = await _mediator.Send(users);
        return Ok(result);
    }

    /// <summary>
    /// Search for customers
    /// </summary>
    /// <returns>List of customers match to keyword and filters</returns>
    [HttpPost("search")]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(PaginatedResult<UserViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchCustomerAsync([FromBody] CustomerSearchQuery query)
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
        var command = new CustomerDeleteByIdCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Updates a customer status
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User information</returns>
    [HttpPut("update-status/{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatusAsync(Guid id)
    {
        var command = new CustomerUpdateStatusCommand { Id = id };

        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("phone/{phone}")]
    [Authorize(Roles = "Employee, Admin")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatusAsync(string phone)
    {
        var command = new CustomerGetByPhoneQuery { phoneNumber = phone};   

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
