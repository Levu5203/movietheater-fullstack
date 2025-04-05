using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.UnitOfWorks;
using AutoMapper;
using MovieTheater.Business.ViewModels.Invoice;

namespace MovieTheater.Business.Handlers.Invoice;

public class InvoiceGetAllQueryHandler : BaseHandler, IRequestHandler<InvoiceGetAllQuery, IEnumerable<InvoiceViewModel>>
{
    public InvoiceGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<IEnumerable<InvoiceViewModel>> Handle(InvoiceGetAllQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.InvoiceRepository.GetQuery()
            .Include(i => i.User)
            .Include(i => i.ShowTime)
                .ThenInclude(s => s!.ShowTimeSlot)
            .Include(i => i.CinemaRoom)
            .Include(i => i.Movie)
            .Include(i => i.Tickets)
                .ThenInclude(t => t.Seat)
            .Include(i => i.Tickets)
                .ThenInclude(t => t.Promotion)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<InvoiceViewModel>>(invoices);
    }
}
