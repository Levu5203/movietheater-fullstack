using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Invoice;

public class CancelExpiredInvoicesHandler : BaseHandler, IRequestHandler<CancelExpiredInvoicesCommand, TimeoutPendingSeatsResult>
{
    private readonly IUserIdentity _userIdentity;

    public CancelExpiredInvoicesHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserIdentity userIdentity)
        : base(unitOfWork, mapper)
    {
        _userIdentity = userIdentity;
    }

    public async Task<TimeoutPendingSeatsResult> Handle(CancelExpiredInvoicesCommand request, CancellationToken cancellationToken)
    {
        var result = new TimeoutPendingSeatsResult
        {
            Success = true,
            Message = "Pending seats timeout processed successfully"
        };

        try
        {
            // Lấy thời điểm timeout
            var timeoutThreshold = DateTime.Now.AddMinutes(-request.ExpiredMinutes);

            // Lấy các invoice chưa thanh toán và được tạo cách đây hơn thời gian timeout
            var expiredInvoices = await _unitOfWork.InvoiceRepository.GetQuery()
                .Where(i => i.IsActive == true
                && i.CreatedAt <= timeoutThreshold
                && i.UserId == _userIdentity.UserId)
                .ToListAsync(cancellationToken);

            if (!expiredInvoices.Any())
            {
                result.Message = "No expired pending invoices found.";
                return result;
            }

            int totalResetSeats = 0;
            int totalRemovedTickets = 0;

            foreach (var invoice in expiredInvoices)
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    // Lấy tất cả ticket liên quan đến invoice này
                    var tickets = await _unitOfWork.TicketRepository.GetQuery()
                        .Where(t => t.InvoiceId == invoice.Id)
                        .ToListAsync(cancellationToken);

                    // Lấy tất cả seat ID từ các ticket
                    var seatIds = tickets.Select(t => t.SeatId).ToList();

                    // Lấy các ghế cần reset trạng thái
                    var seats = await _unitOfWork.SeatRepository.GetQuery()
                        .Where(s => seatIds.Contains(s.Id))
                        .ToListAsync(cancellationToken);

                    // Reset trạng thái ghế về Available
                    foreach (var seat in seats)
                    {
                        seat.seatStatus = SeatStatus.Available;
                        seat.UpdatedAt = DateTime.Now;
                        _unitOfWork.SeatRepository.Update(seat);
                    }

                    // Xóa các ticket
                    foreach (var ticket in tickets)
                    {
                        _unitOfWork.TicketRepository.Delete(ticket, true);
                    }

                    // Xóa invoice
                    _unitOfWork.InvoiceRepository.Delete(invoice, true);

                    // Lưu các thay đổi
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);

                    // Cập nhật kết quả
                    totalResetSeats += seats.Count();
                    totalRemovedTickets += tickets.Count();
                    result.ProcessedInvoiceIds.Add(invoice.Id);

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    // Tiếp tục xử lý các invoice khác dù có lỗi với invoice này
                }
            }

            // Cập nhật kết quả cuối cùng
            result.ProcessedInvoicesCount = expiredInvoices.Count();
            result.ResetSeatsCount = totalResetSeats;
            result.RemovedTicketsCount = totalRemovedTickets;

            return result;
        }
        catch (Exception ex)
        {
            return new TimeoutPendingSeatsResult
            {
                Success = false,
                Message = $"Error processing timeout: {ex.Message}"
            };
        }
    }
}
