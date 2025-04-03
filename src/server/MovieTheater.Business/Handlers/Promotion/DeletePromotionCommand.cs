using System;
using MediatR;

namespace MovieTheater.Business.Handlers.Promotion;

public class DeletePromotionCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeletePromotionCommand(Guid id)
    {
        Id = id;
    }
}
