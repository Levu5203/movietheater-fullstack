using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Invoice;

public class InvoiceIssueTicketCommandHandler : BaseHandler, IRequestHandler<InvoiceIssueTicketCommand, InvoiceViewModel>
{
    public InvoiceIssueTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<InvoiceViewModel> Handle(InvoiceIssueTicketCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.InvoiceRepository.GetQuery()
            .Include(i => i.Tickets)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (invoice == null || invoice.IsDeleted)
        {
            throw new KeyNotFoundException("Seat not found.");
        }

        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            foreach (var ticket in invoice.Tickets)
            {
                ticket.Status = TicketStatus.Issued;
            }
            invoice.TicketIssued = true;

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return _mapper.Map<InvoiceViewModel>(invoice);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
