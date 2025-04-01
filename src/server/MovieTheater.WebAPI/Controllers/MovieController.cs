using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Movie;
using MovieTheater.Business.ViewModels.Movie;

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

    public class MovieController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        /// <summary>
        /// Retrieves all orders (admin only).
        /// </summary>
        /// <returns>A collection of all orders.</returns>
        [HttpGet("movies")]
        [ProducesResponseType(typeof(IEnumerable<MovieViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new MovieGetAllQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            var query = new MovieGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
