using System;
using MediatR;
using MovieTheater.Business.Services;
using MovieTheater.Data;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Promotion;
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        public UpdatePromotionCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<bool> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _unitOfWork.PromotionRepository.GetByIdAsync(request.Id);
            if (promotion == null)
            {
                return false;
            }

            // Cập nhật thông tin
            promotion.PromotionTitle = request.PromotionTitle;
            promotion.Description = request.Description;
            promotion.Discount = request.Discount;
            promotion.StartDate = request.StartDate;
            promotion.EndDate = request.EndDate;

            // Nếu có ảnh mới, cập nhật ảnh
            if (request.Image != null)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(promotion.Image))
                {
                    await _fileStorageService.DeleteFileAsync(promotion.Image);
                }

                // Lưu ảnh mới
                promotion.Image = await _fileStorageService.SaveFileAsync(request.Image);
            }

            _unitOfWork.PromotionRepository.Update(promotion);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }



