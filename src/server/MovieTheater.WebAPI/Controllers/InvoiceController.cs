using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Invoice;
using MovieTheater.Business.Handlers.Seat;
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
        [HttpPost("payment")]
        [Authorize(Roles = "Employee, Customer")]
        [ProducesResponseType(typeof(InvoicePreviewViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PaymentAsync([FromBody] SeatPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
