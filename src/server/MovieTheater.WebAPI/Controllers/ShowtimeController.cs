using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Showtime;
using MovieTheater.Business.ViewModels.Showtime;

namespace MovieTheater.WebAPI.Controllers
{
    /// <summary>
    /// Authen API Controller 
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class ShowtimeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet("showtimes")]
        [ProducesResponseType(typeof(IEnumerable<ShowtimeViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new ShowtimeGetAllQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ShowtimeViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new ShowtimeGetByIdQuery{Id = id};
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
