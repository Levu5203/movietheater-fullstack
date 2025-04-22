using MediatR;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Promotion;

public class DeletePromotionHandler : IRequestHandler<DeletePromotionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePromotionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = await _unitOfWork.PromotionRepository.GetByIdAsync(request.Id);
        if (promotion == null)
            return false; // Trả về false nếu không tìm thấy

        _unitOfWork.PromotionRepository.Delete(promotion);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
