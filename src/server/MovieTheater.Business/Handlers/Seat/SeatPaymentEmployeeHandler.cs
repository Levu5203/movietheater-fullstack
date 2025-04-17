using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Core.Exceptions;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatPaymentEmployeeHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : BaseHandler(_unitOfWork, _mapper), IRequestHandler<SeatPaymentEmployeeCommand, InvoicePreviewViewModel>
{
    public async Task<InvoicePreviewViewModel> Handle(SeatPaymentEmployeeCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) throw new ResourceNotFoundException("Invoice not found");
            if (invoice.InvoiceStatus == InvoiceStatus.Paid) throw new InvalidOperationException("Invoice is already paid");

            // 🔍 Tìm user theo số điện thoại
            if (request.PhoneNumber != null)
            {
                var customer = await _unitOfWork.UserRepository.GetQuery()
                    .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);
                if (customer != null)
                {
                    invoice.User = customer;
                }
            }

            var tickets = await _unitOfWork.TicketRepository.GetQuery()
                .Where(t => t.InvoiceId == request.InvoiceId)
                .ToListAsync(cancellationToken);
            if (tickets.Count == 0) throw new ResourceNotFoundException("Tickets not found");

            var seatIds = tickets.Select(t => t.SeatId).ToList();
            var showtimeId = tickets.First().ShowTimeId;

            var seatShowtimes = await _unitOfWork.SeatShowtimeRepository.GetQuery()
                .Where(s => seatIds.Contains(s.SeatId) && s.ShowTimeId == showtimeId)
                .ToListAsync(cancellationToken);

            foreach (var seatShowtime in seatShowtimes)
            {
                seatShowtime.Status = SeatStatus.Booked;
                seatShowtime.UpdatedAt = DateTime.Now;
                _unitOfWork.SeatShowtimeRepository.Update(seatShowtime);
            }

            foreach (var ticket in tickets)
            {
                ticket.Status = TicketStatus.Paid;
                _unitOfWork.TicketRepository.Update(ticket);
            }

            invoice.InvoiceStatus = InvoiceStatus.Paid;
            invoice.UpdatedAt = DateTime.Now;
            _unitOfWork.InvoiceRepository.Update(invoice);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return _mapper.Map<InvoicePreviewViewModel>(invoice);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
