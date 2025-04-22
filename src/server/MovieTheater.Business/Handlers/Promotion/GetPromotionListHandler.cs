using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Promotion;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Promotion;

public class GetPromotionListHandler : IRequestHandler<GetPromotionListQuery, List<PromotionViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPromotionListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<PromotionViewModel>> Handle(GetPromotionListQuery request, CancellationToken cancellationToken)
    {
        var promotions = await _unitOfWork.PromotionRepository.GetQuery().ToListAsync(cancellationToken);

        return promotions.Select(promotion => new PromotionViewModel{
            Id = promotion.Id,
            PromotionTitle = promotion.PromotionTitle,
            Description = promotion.Description,
            Discount = promotion.Discount,
            StartDate = promotion.StartDate,
            EndDate = promotion.EndDate,
            Image = promotion.Image
        }).ToList();
    }
}
