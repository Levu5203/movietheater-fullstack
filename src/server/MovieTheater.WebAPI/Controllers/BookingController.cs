using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTheater.Business.Handlers.Invoice;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Core;

namespace MovieTheater.WebAPI.Controllers
{
    /// <summary>
    /// Handles all booking-related operations, including invoice retrieval and ticket issuance.
    /// </summary>
    [Route("api/v{version:apiVersion}/bookings")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Employee")]

    public class BookingController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves all invoices.
        /// </summary>
        /// <returns>A list of all invoices.</returns>
        /// <response code="200">Returns the list of invoices.</response>
        [HttpGet("")]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInvoice()
        {
            var query = new InvoiceGetAllQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Searches invoices based on provided criteria.
        /// </summary>
        /// <param name="query">The invoice search criteria.</param>
        /// <returns>A paginated list of matching invoices.</returns>
        /// <response code="200">Returns the paginated search results.</response>
        [HttpPost("search")]
        [ProducesResponseType(typeof(PaginatedResult<InvoiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchInvoiceAsync([FromBody] InvoiceSearchQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific invoice by its ID.
        /// </summary>
        /// <param name="id">The ID of the invoice to retrieve.</param>
        /// <returns>The requested invoice.</returns>
        /// <response code="200">Returns the invoice with the specified ID.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoiceById(Guid id)
        {
            var query = new InvoiceGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Issues tickets for a specific invoice.
        /// </summary>
        /// <param name="id">The ID of the invoice to issue tickets for.</param>
        /// <returns>The updated invoice with issued tickets.</returns>
        /// <response code="200">Returns the invoice after issuing tickets.</response>
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
