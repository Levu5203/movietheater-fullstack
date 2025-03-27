using System;
using MediatR;
using MovieTheater.Business.ViewModels.Ticket;

namespace MovieTheater.Business.Handlers.Ticket;

public class GetBookedTicketsByMemberIdQuery : IRequest<List<BookedTicketViewModel>>
{
    public Guid MemberId { get; }

    public GetBookedTicketsByMemberIdQuery(Guid memberId)
    {
        MemberId = memberId;
    }
}


