using System;
using MediatR;

namespace MovieTheater.Business.Handlers.Invoice;

public class CancelExpiredInvoicesCommand : IRequest<TimeoutPendingSeatsResult>
{
    public int ExpiredMinutes { get; set; } = 3;
}
