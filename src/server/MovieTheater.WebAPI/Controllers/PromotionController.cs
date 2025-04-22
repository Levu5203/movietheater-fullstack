using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Promotion;
using MovieTheater.Business.ViewModels.Promotion;

namespace MovieTheater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
         private readonly IMediator _mediator;

        public PromotionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PromotionViewModel>>> GetPromotions()
        {
            var promotions = await _mediator.Send(new GetPromotionListQuery());
            return Ok(promotions);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePromotion([FromForm] CreatePromotionCommand command)
        {
            var promotionId = await _mediator.Send(command);
            return Ok(new { Id = promotionId, Message = "Promotion created successfully" });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePromotion(Guid id, [FromForm] UpdatePromotionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Promotion ID mismatch.");
            }

            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound("Promotion not found.");
            }

            return Ok(new { message = "Promotion updated successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotionById(Guid id)
        {
            var result = await _mediator.Send(new GetPromotionByIdQuery(id));

            if (result == null)
            {
                return NotFound(new { message = "Promotion not found" });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            var result = await _mediator.Send(new DeletePromotionCommand(id));

            if (!result)
                return NotFound("Promotion not found.");

            return Ok(new { message = "Promotion deleted successfully" });
        }
    }
}
