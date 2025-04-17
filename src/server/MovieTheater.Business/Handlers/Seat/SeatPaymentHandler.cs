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

public class SeatPaymentHandler(IUnitOfWork _unitOfWork, IMapper mapper, IUserIdentity userIdentity) : BaseHandler(_unitOfWork, mapper), IRequestHandler<SeatPaymentCommand, InvoicePreviewViewModel>
{
    private readonly IUserIdentity currentUser = userIdentity;

    public async Task<InvoicePreviewViewModel> Handle(SeatPaymentCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) throw new ResourceNotFoundException("Invoice not found");
            if (invoice.InvoiceStatus == InvoiceStatus.Paid) throw new InvalidOperationException("Invoice is already paid");

            // Kiểm tra nếu invoice thuộc về người dùng hiện tại
            if (invoice.UserId != currentUser.UserId) throw new UnauthorizedAccessException("You are not authorized to pay this invoice");

            var tickets = await _unitOfWork.TicketRepository.GetQuery()
                .Where(t => t.InvoiceId == request.InvoiceId)
                .ToListAsync(cancellationToken);
            if (tickets.Count == 0) throw new ResourceNotFoundException("Tickets not found");

            // Cập nhật trạng thái ghế thành Booked
            var seatIds = tickets.Select(t => t.SeatId).ToList();
            var showtimeId = tickets.First().ShowTimeId;
            // Cập nhật trạng thái ghế tạm thời (bảng SeatShowtime) thành Booked
            var seatShowtimes = await _unitOfWork.SeatShowtimeRepository.GetQuery()
                .Where(s => seatIds.Contains(s.SeatId) && s.ShowTimeId == showtimeId)
                .ToListAsync(cancellationToken);

            foreach (var seatShowtime in seatShowtimes)
            {
                seatShowtime.Status = SeatStatus.Booked;
                seatShowtime.UpdatedById = currentUser.UserId;
                seatShowtime.UpdatedAt = DateTime.Now;
                _unitOfWork.SeatShowtimeRepository.Update(seatShowtime);
            }

            // Cập nhật trạng thái vé thành Paid
            foreach (var ticket in tickets)
            {
                ticket.Status = TicketStatus.Paid;
                _unitOfWork.TicketRepository.Update(ticket);
            }

            // Cập nhật trạng thái invoice thành Paid
            invoice.InvoiceStatus = InvoiceStatus.Paid;
            invoice.UpdatedById = currentUser.UserId;
            invoice.UpdatedAt = DateTime.Now;
            _unitOfWork.InvoiceRepository.Update(invoice);

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return _mapper.Map<InvoicePreviewViewModel>(invoice);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception("An error occurred while processing the payment", e);
        }
    }
}
