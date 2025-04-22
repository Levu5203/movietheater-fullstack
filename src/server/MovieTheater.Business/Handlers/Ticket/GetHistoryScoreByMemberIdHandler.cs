using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Ticket;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Ticket
{
    public class GetHistoryScoreByUserIdCommandHandler 
        : IRequestHandler<GetHistoryScoreByMemberIdQuery, List<HistoryScoreViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHistoryScoreByUserIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HistoryScoreViewModel>> Handle(
            GetHistoryScoreByMemberIdQuery request, CancellationToken cancellationToken)
        {
            var historyScores = await _unitOfWork.HistoryScoreRepository
                .GetQuery()
                .Include(hs => hs.Invoice)
                    .ThenInclude(i => i.Tickets)  // Load tickets của invoice
                .Include(hs => hs.Invoice)
                    .ThenInclude(i => i.ShowTime) // Load showtime của invoice
                    .ThenInclude(s => s.Movie)    // Load movie của showtime
                .Where(hs => hs.Invoice.UserId == request.MemberId)
                .Select(hs => new HistoryScoreViewModel
                {
                    MovieName = hs.Invoice.ShowTime.Movie.Name,
                    BookingDate = hs.Invoice.Tickets.FirstOrDefault().BookingDate,
                    AddedScore = hs.Invoice.AddedScore,
                    Status = hs.ScoreStatus
                })
                .ToListAsync(cancellationToken);

            return historyScores;
        }
    }
}
