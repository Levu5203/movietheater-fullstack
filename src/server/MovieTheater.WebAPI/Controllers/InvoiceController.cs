using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Invoice;
using MovieTheater.Business.ViewModels.Invoice;

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

    public class InvoiceController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InvoicePreviewViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoicePreviewById(Guid id)
        {
            var query = new InvoicePreviewGetByIdQuery { InvoiceId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
