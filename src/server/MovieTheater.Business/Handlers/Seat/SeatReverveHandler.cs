using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.Manager;
using MovieTheater.Business.ViewModels.Invoice;
using MovieTheater.Core.Exceptions;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatReverveHandler(IUnitOfWork _unitOfWork, IMapper mapper, IUserIdentity userIdentity, ShowtimeQueueManager queueManager) : BaseHandler(_unitOfWork, mapper),
                                IRequestHandler<SeatReverveCommand, InvoicePreviewViewModel>
{
    private readonly IUserIdentity currentUser = userIdentity;
    public async Task<InvoicePreviewViewModel> Handle(SeatReverveCommand request, CancellationToken cancellationToken)
    {
        return await queueManager.EnqueueAsync(request.ShowTimeId, async () =>
        {
        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var showtime = await _unitOfWork.ShowtimeRepository.GetQuery()
                        .Include(st => st.CinemaRoom)
                        .Include(st => st.Movie)
                        .FirstOrDefaultAsync(st => st.Id == request.ShowTimeId, cancellationToken);//thêm include để cinema với movie không bị null
            if (showtime == null) throw new ResourceNotFoundException("Showtime not found");
            var seats = await _unitOfWork.SeatRepository.GetQuery()
                .Where(s => request.SeatIds.Contains(s.Id))
                .ToListAsync(cancellationToken);
            if (seats.Count != request.SeatIds.Count) throw new ResourceNotFoundException("Seat not found");
            var invoice = new Models.Common.Invoice
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.UserId,
                CinemaRoomId = showtime.CinemaRoomId,
                MovieId = showtime.MovieId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedById = currentUser.UserId,
                UpdatedById = currentUser.UserId,
                TotalMoney = 0,
                AddedScore = 0,
                User = await _unitOfWork.UserRepository.GetByIdAsync(currentUser.UserId),
                ShowTimeId = showtime.Id,
                InvoiceStatus = InvoiceStatus.Pending
            };
            var seatShowTimes = await _unitOfWork.SeatShowtimeRepository.GetQuery()
                .Where(sst => sst.ShowTimeId == showtime.Id && request.SeatIds.Contains(sst.SeatId))
                .ToListAsync(cancellationToken);
            foreach (var seat in seats)
            {
                var existingSST = seatShowTimes.FirstOrDefault(sst => sst.SeatId == seat.Id);

                if (existingSST != null)
                {
                    // Cập nhật trạng thái nếu đã có bản ghi
                    existingSST.Status = SeatStatus.Pending;
                    existingSST.UpdatedAt = DateTime.Now;
                    existingSST.UpdatedById = currentUser.UserId;
                    _unitOfWork.SeatShowtimeRepository.Update(existingSST);
                }
                else
                {
                    // Thêm bản ghi mới nếu chưa có
                    var newSST = new SeatShowTime
                    {
                        SeatShowTimeId = Guid.NewGuid(),
                        SeatId = seat.Id,
                        ShowTimeId = showtime.Id,
                        Status = SeatStatus.Pending,
                        UpdatedAt = DateTime.Now,
                        UpdatedById = currentUser.UserId
                    };
                    _unitOfWork.SeatShowtimeRepository.Add(newSST);
                }
            }
            var tickets = seats.Select(seat => new Models.Common.Ticket
            {
                Id = Guid.NewGuid(),
                SeatId = seat.Id,
                Price = seat.SeatType == SeatType.VIP ? 70000 : 50000,
                Status = TicketStatus.Pending,
                BookingDate = DateTime.Now,
                InvoiceId = invoice.Id,
                ShowTimeId = showtime.Id,
                MovieId = showtime.MovieId,
                CinemaRoomId = showtime.CinemaRoomId,
            }).ToList();
            foreach (var ticket in tickets)
            {
                _unitOfWork.TicketRepository.Add(ticket);
            }
            invoice.TotalMoney = tickets.Sum(t => t.Price);
            invoice.AddedScore = tickets.Count * 10;
            _unitOfWork.InvoiceRepository.Add(invoice);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return _mapper.Map<InvoicePreviewViewModel>(invoice);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception("An error occurred while reserving seats", e);
        }
    });
    }
}
