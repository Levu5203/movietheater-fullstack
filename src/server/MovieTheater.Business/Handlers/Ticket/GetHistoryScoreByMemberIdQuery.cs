using MovieTheater.Business.ViewModels.Ticket;

namespace MovieTheater.Business.Handlers.Ticket;

public class GetHistoryScoreByMemberIdQuery : BaseGetByIdQuery<List<HistoryScoreViewModel>>
{
    public Guid MemberId { get; }

    public GetHistoryScoreByMemberIdQuery(Guid memberId)
    {
        MemberId = memberId;
    }
}
