using MediatR;
using MovieTheater.Business.Services;
using MovieTheater.Data.UnitOfWorks;
using DomainPromotion = MovieTheater.Models.Common.Promotion;

namespace MovieTheater.Business.Handlers.Promotion;

public class CreatePromotionHandler : IRequestHandler<CreatePromotionCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAzureService _azureService;

    public CreatePromotionHandler(IUnitOfWork unitOfWork, IAzureService azureService)
    {
        _unitOfWork = unitOfWork;
        _azureService = azureService;
    }
    public async Task<Guid> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
{
    // Handle image upload
    var fileName = $"/{Guid.NewGuid()}_{request.Image!.FileName}";
    var promotionUrl = await _azureService.UploadFileAsync(request.Image!, fileName);

    var promotion = new DomainPromotion
    {
        Id = Guid.NewGuid(),
        PromotionTitle = request.PromotionTitle!,
        Description = request.Description!,
        Discount = request.Discount,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        Image = promotionUrl
    };

    await _unitOfWork.PromotionRepository.AddAsync(promotion);
    await _unitOfWork.SaveChangesAsync();

    return promotion.Id;
}

}
