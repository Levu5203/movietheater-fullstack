using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Invoice;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Core;

namespace MovieTheater.WebAPI.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BookingController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("")]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInvoice()
        {
            var query = new InvoiceGetAllQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(PaginatedResult<InvoiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchInvoiceAsync([FromBody] InvoiceSearchQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoiceById(Guid id)
        {
            var query = new InvoiceGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> IssueTickets(Guid id)
        {
            var query = new InvoiceIssueTicketCommand { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
