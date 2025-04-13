using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Core.Exceptions;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatPaymentEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler(unitOfWork, mapper), IRequestHandler<SeatPaymentEmployeeCommand, InvoicePreviewViewModel>
{
    public async Task<InvoicePreviewViewModel> Handle(SeatPaymentEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            var invoice = await unitOfWork.InvoiceRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) throw new ResourceNotFoundException("Invoice not found");
            if (invoice.InvoiceStatus == InvoiceStatus.Paid) throw new InvalidOperationException("Invoice is already paid");

            // 🔍 Tìm user theo số điện thoại
            var customer = await unitOfWork.UserRepository.GetQuery()
                .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);

            if (customer != null){
                invoice.User = customer;
            }
            var tickets = await unitOfWork.TicketRepository.GetQuery()
                .Where(t => t.InvoiceId == request.InvoiceId)
                .ToListAsync(cancellationToken);
            if (tickets.Count == 0) throw new ResourceNotFoundException("Tickets not found");

            var seatIds = tickets.Select(t => t.SeatId).ToList();
            var showtimeId = tickets.First().ShowTimeId;

            var seatShowtimes = await unitOfWork.SeatShowtimeRepository.GetQuery()
                .Where(s => seatIds.Contains(s.SeatId) && s.ShowTimeId == showtimeId)
                .ToListAsync(cancellationToken);

            foreach (var seatShowtime in seatShowtimes)
            {
                seatShowtime.Status = SeatStatus.Booked;
                seatShowtime.UpdatedAt = DateTime.Now;
                unitOfWork.SeatShowtimeRepository.Update(seatShowtime);
            }

            foreach (var ticket in tickets)
            {
                ticket.Status = TicketStatus.Paid;
                unitOfWork.TicketRepository.Update(ticket);
            }

            invoice.InvoiceStatus = InvoiceStatus.Paid;
            invoice.UpdatedAt = DateTime.Now;
            unitOfWork.InvoiceRepository.Update(invoice);

            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return mapper.Map<InvoicePreviewViewModel>(invoice);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}   
