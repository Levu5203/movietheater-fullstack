using MediatR;
using Microsoft.AspNetCore.Http;
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
    }
}
