using System;
using AutoMapper;
using MediatR;
using MovieTheater.Business.ViewModels.Promotion;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Promotion;

public class GetPromotionByIdHandler : IRequestHandler<GetPromotionByIdQuery, PromotionViewModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPromotionByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PromotionViewModel> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
    {
        var promotion = await _unitOfWork.PromotionRepository.GetByIdAsync(request.Id);

        if (promotion == null)
        {
            return null;
        }

        return _mapper.Map<PromotionViewModel>(promotion);
    }
}
