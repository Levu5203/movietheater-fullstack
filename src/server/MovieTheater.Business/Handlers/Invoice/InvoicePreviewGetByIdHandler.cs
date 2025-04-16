using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Invoice;

public class InvoicePreviewGetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserIdentity currentUser) : BaseHandler(unitOfWork, mapper),
                                                            IRequestHandler<InvoicePreviewGetByIdQuery, InvoicePreviewViewModel>
{
    private readonly IUserIdentity _currentUser = currentUser;
    public async Task<InvoicePreviewViewModel> Handle(InvoicePreviewGetByIdQuery request, CancellationToken cancellationToken){
       var invoice = await _unitOfWork.InvoiceRepository.GetQuery()
            .Include(i => i.Tickets)
                .ThenInclude(t => t.Seat)
            .Include(i => i.ShowTime)
                .ThenInclude(s => s.CinemaRoom)
            .Include(i => i.ShowTime)
                .ThenInclude(s => s.Movie)
            .Include(i => i.ShowTime)
                .ThenInclude(s => s.ShowTimeSlot)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);
        
        if (invoice == null)
        {
            throw new KeyNotFoundException($"Invoice with ID {request.InvoiceId} not found.");
        }

        return _mapper.Map<InvoicePreviewViewModel>(invoice);
    }
}
