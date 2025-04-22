using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.CinemaRoom;
using MovieTheater.Business.Handlers.Seat;
using MovieTheater.Business.ViewModels.Room;
using MovieTheater.Business.ViewModels.Seat;
using MovieTheater.Core;

namespace MovieTheater.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/rooms")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class RoomController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get list of rooms
        /// </summary>
        /// <returns>List of rooms</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CinemaRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoomAsync()
        {
            var rooms = new CinemaRoomsGetAllQuery();
            var result = await _mediator.Send(rooms);
            return Ok(result);
        }

        /// <summary>
        /// Search for rooms
        /// </summary>
        /// <returns>List of rooms match to keyword and filters</returns>
        [HttpPost("search")]
        [ProducesResponseType(typeof(PaginatedResult<CinemaRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchRoomAsync([FromBody] CinemaRoomSearchQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get of a room by id
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns>Room view model</returns>
        [HttpGet("{roomId}")]
        [ProducesResponseType(typeof(IEnumerable<SeatViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomByIdAsync(Guid roomId)
        {
            var query = new CinemaRoomGetByIdQuery { Id = roomId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get seats of a room
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns>List of seats in the room</returns>
        [HttpGet("{roomId}/seats")]
        [ProducesResponseType(typeof(IEnumerable<SeatViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSeatsByRoomAsync(Guid roomId)
        {
            var query = new SeatGetByRoomIdQuery { RoomId = roomId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Update seat types for a room
        /// </summary>
        /// <param name="command">List of seats to update</param>
        /// <returns>Status</returns>
        [HttpPut("{roomId}/update-seats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSeatsAsync([FromBody] MultipleSeatsUpdateCommand command)
        {
            if (command == null || command.Commands == null || !command.Commands.Any())
                return BadRequest("No seat data provided.");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}