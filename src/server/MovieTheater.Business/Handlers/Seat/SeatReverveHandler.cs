using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Ticket;
using MovieTheater.Core.Exceptions;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Common;

namespace MovieTheater.Business.Handlers.Seat;

public class SeatReverveHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserIdentity userIdentity) : BaseHandler(unitOfWork, mapper),
                                IRequestHandler<SeatReverveCommand, IEnumerable<TicketViewModel>>
{
    private readonly IUserIdentity currentUser = userIdentity;
    public async Task<IEnumerable<TicketViewModel>> Handle(SeatReverveCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            var showtime = await _unitOfWork.ShowtimeRepository.GetByIdAsync(request.ShowTimeId);
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
            };
            //set seats to pendding
            foreach (var seat in seats)
            {
                seat.seatStatus = SeatStatus.Pending;
                seat.UpdatedById = currentUser.UserId;
                seat.UpdatedAt = DateTime.Now;
                _unitOfWork.SeatRepository.Update(seat);
            }
            var tickets = seats.Select(seat => new Models.Common.Ticket
            {
                Id = Guid.NewGuid(),
                SeatId = seat.Id,
                Price = 50000,
                Status = TicketStatus.Paid,
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
            await unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return _mapper.Map<IEnumerable<TicketViewModel>>(tickets);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
