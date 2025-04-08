using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Invoice;

public class InvoiceGetByIdQueryHandler : BaseHandler, IRequestHandler<InvoiceGetByIdQuery, InvoiceViewModel>
{
    public InvoiceGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<InvoiceViewModel> Handle(InvoiceGetByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.InvoiceRepository.GetQuery()
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
            .FirstOrDefaultAsync(x => x.Id == request.Id);


        if (invoice == null || invoice.IsDeleted)
        {
            throw new KeyNotFoundException("Invoice not found");
        }
        return _mapper.Map<InvoiceViewModel>(invoice);
    }
}
