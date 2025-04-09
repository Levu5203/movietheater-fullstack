using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Seat;
using MovieTheater.Business.ViewModels.Ticket;

namespace MovieTheater.WebAPI.Controllers
{
    /// <summary>
    /// Authen API Controller 
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class SeatController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("reserve")]
        [ProducesResponseType(typeof(IEnumerable<TicketViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReserveAsync([FromBody] SeatReverveCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
