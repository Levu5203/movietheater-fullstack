using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.CinemaRoom;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Core;

namespace MovieTheater.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/rooms")]
    [ApiController]
    [ApiVersion("1.0")]
    // [Authorize]
    public class RoomController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get list of rooms
        /// </summary>
        /// <returns>List of rooms</returns>
        [HttpGet]
        // [Authorize(Roles = "Admin, Employee")]
        [ProducesResponseType(typeof(IEnumerable<CinemaRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoomAsync()
        {
            var rooms = new CinemaRoomsGetAllQuery();
            var result = await _mediator.Send(rooms);
            return Ok(result);
        }

        [HttpPost("search")]
        // [Authorize(Roles = "Admin, Employee")]
        [ProducesResponseType(typeof(PaginatedResult<CinemaRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchCustomerAsync([FromBody] CinemaRoomSearchQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
