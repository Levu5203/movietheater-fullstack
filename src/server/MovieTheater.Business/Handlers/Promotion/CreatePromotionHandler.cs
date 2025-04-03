using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using MovieTheater.Data.UnitOfWorks;
using DomainPromotion = MovieTheater.Models.Common.Promotion;

namespace MovieTheater.Business.Handlers.Promotion;

public class CreatePromotionHandler : IRequestHandler<CreatePromotionCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _env; 

    public CreatePromotionHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }
    public async Task<Guid> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
{
    if (request.Image == null)
    {
        return Guid.Empty; // Trả về một GUID rỗng nếu không có ảnh
    }

    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
    if (!Directory.Exists(uploadsFolder))
    {
        Directory.CreateDirectory(uploadsFolder);
    }

    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
        await request.Image.CopyToAsync(fileStream);
    }

    string imageUrl = $"/uploads/{uniqueFileName}";

    var promotion = new DomainPromotion
    {
        Id = Guid.NewGuid(),
        PromotionTitle = request.PromotionTitle,
        Description = request.Description,
        Discount = request.Discount,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        Image = imageUrl
    };

    await _unitOfWork.PromotionRepository.AddAsync(promotion);
    await _unitOfWork.SaveChangesAsync();

    return promotion.Id;
}

}
