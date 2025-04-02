using AutoMapper;
using MediatR;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Employees;

public class EmployeeGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseHandler(unitOfWork, mapper),
    IRequestHandler<EmployeeGetByIdQuery, UserViewModel>
{
    public async Task<UserViewModel> Handle(EmployeeGetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.UserRepository.GetByIdAsync(request.Id) ??
                    throw new KeyNotFoundException($"User with {request.Id} is not found");

        return _mapper.Map<UserViewModel>(entity);
    }
}