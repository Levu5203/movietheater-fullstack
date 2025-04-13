using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Employees;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Core;

namespace MovieTheater.WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class EmployeesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Get list of Employees
    /// </summary>
    /// <returns>List of Employees</returns>
    [HttpGet()]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllEmployeeAsync()
    {
        var users = new EmployeeGetAllQuery();
        var result = await _mediator.Send(users);
        return Ok(result);
    }

    /// <summary>
    /// Search for Employees
    /// </summary>
    /// <returns>List of Employees match to keyword and filters</returns>
    [HttpPost("search")]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(PaginatedResult<EmployeeViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchEmployeeAsync([FromBody] EmployeeSearchQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get employee by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Employee with id</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        var query = new EmployeeGetByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Deletes a Employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns>boolean</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new EmployeeDeleteByIdCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new employee
    /// </summary>
    /// <returns>Employee created</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(EmployeeViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromForm] EmployeeCreateCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }

    /// <summary>
    /// Updates a employee
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns>User information</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(EmployeeViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] EmployeeUpdateCommand command)
    {
        command.Id = id;
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Updates a employee status
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User information</returns>
    [HttpPut("update-status/{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(EmployeeViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatusAsync(Guid id)
    {
        var command = new EmployeeUpdateStatusCommand { Id = id };

        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
