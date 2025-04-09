using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Core.Exceptions;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatPaymentHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserIdentity userIdentity) : BaseHandler(unitOfWork, mapper), IRequestHandler<SeatPaymentCommand, InvoicePreviewViewModel>
{
    private readonly IUserIdentity currentUser = userIdentity;

    public async Task<InvoicePreviewViewModel> Handle(SeatPaymentCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            var invoice = await unitOfWork.InvoiceRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) throw new ResourceNotFoundException("Invoice not found");
            if (invoice.IsActive == false) throw new InvalidOperationException("Invoice is already paid");

            // Kiểm tra nếu invoice thuộc về người dùng hiện tại
            if (invoice.UserId != currentUser.UserId) throw new UnauthorizedAccessException("You are not authorized to pay this invoice");

            var tickets = await unitOfWork.TicketRepository.GetQuery()
                .Where(t => t.InvoiceId == request.InvoiceId)
                .ToListAsync(cancellationToken);
            if (tickets.Count == 0) throw new ResourceNotFoundException("Tickets not found");

            // Cập nhật trạng thái ghế thành Booked
            var seats = await unitOfWork.SeatRepository.GetQuery()
                .Where(s => tickets.Select(t => t.SeatId).Contains(s.Id))
                .ToListAsync(cancellationToken);

            foreach (var seat in seats)
            {
                seat.seatStatus = SeatStatus.Booked;
                seat.UpdatedById = currentUser.UserId;
                seat.UpdatedAt = DateTime.Now;
                unitOfWork.SeatRepository.Update(seat);
            }

            // Cập nhật trạng thái vé thành Paid
            foreach (var ticket in tickets)
            {
                ticket.Status = TicketStatus.Paid;
                unitOfWork.TicketRepository.Update(ticket);
            }

            // Cập nhật trạng thái invoice thành Paid
            invoice.IsActive = false;
            invoice.UpdatedById = currentUser.UserId;
            invoice.UpdatedAt = DateTime.Now;
            unitOfWork.InvoiceRepository.Update(invoice);

            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return _mapper.Map<InvoicePreviewViewModel>(invoice);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
