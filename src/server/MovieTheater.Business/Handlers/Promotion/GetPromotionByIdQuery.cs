using System;
using MediatR;
using MovieTheater.Business.ViewModels.Promotion;

namespace MovieTheater.Business.Handlers.Promotion;

public class GetPromotionByIdQuery : IRequest<PromotionViewModel>
{
    public Guid Id { get; set; }

    public GetPromotionByIdQuery(Guid id)
    {
        Id = id;
    }
}
