using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Ticket;


namespace MovieTheater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;


        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("booked")]
        [Authorize]
        public async Task<IActionResult> GetBookedTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }

            var query = new GetBookedTicketsByMemberIdQuery(Guid.Parse(userId));
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
