using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.UnitOfWorks;
using AutoMapper;
using MovieTheater.Business.ViewModels.Invoice;

namespace MovieTheater.Business.Handlers.Invoice;

public class PaidInvoiceGetAllQueryHandler : BaseHandler, IRequestHandler<PaidInvoiceGetAllQuery, IEnumerable<InvoiceViewModel>>
{
    public PaidInvoiceGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<IEnumerable<InvoiceViewModel>> Handle(PaidInvoiceGetAllQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.InvoiceRepository.GetQuery()
            .Include(i => i.User)
            .Include(i => i.ShowTime)
                .ThenInclude(s => s!.ShowTimeSlot)
            .Include(i => i.CinemaRoom)
            .Include(i => i.Movie)
            .Include(i => i.Tickets)
                .ThenInclude(t => t.Seat)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<InvoiceViewModel>>(invoices);
    }
}
