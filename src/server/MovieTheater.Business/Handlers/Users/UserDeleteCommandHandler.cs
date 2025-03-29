using AutoMapper;
using MediatR;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Users;

public class UserDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseHandler(unitOfWork, mapper),
    IRequestHandler<UserDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(UserDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.UserRepository.GetByIdAsync(request.Id) ??
                    throw new KeyNotFoundException($"User with {request.Id} is not found");

        _unitOfWork.UserRepository.Delete(entity);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
