using MediatR;
using MovieTheater.Business.Services;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Promotion;
public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IAzureService _azureService;

    public UpdatePromotionCommandHandler(IUnitOfWork unitOfWork, IAzureService azureService)
    {
        _unitOfWork = unitOfWork;

        _azureService = azureService;
    }

    public async Task<bool> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = await _unitOfWork.PromotionRepository.GetByIdAsync(request.Id);
        if (promotion == null)
        {
            return false;
        }

        if (request.Image != null && request.Image.Length > 0)
        {
            var fileName = $"/{Guid.NewGuid()}_{request.Image!.FileName}";
            var posterUrl = await _azureService.UploadFileAsync(request.Image!, fileName);
            promotion.Image = posterUrl;
        }
        // Cập nhật thông tin
        promotion.PromotionTitle = request.PromotionTitle ?? string.Empty;
        promotion.Description = request.Description ?? string.Empty;
        promotion.Discount = request.Discount;
        promotion.StartDate = request.StartDate;
        promotion.EndDate = request.EndDate;

        _unitOfWork.PromotionRepository.Update(promotion);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}



