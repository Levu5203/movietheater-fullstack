using AutoMapper;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers;

public class BaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;

    protected readonly IMapper _mapper = mapper;
}