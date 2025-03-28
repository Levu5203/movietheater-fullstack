using AutoMapper;
using MediatR;
using MovieTheater.Data;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Profile;

public class GetProfileByIdQueryHandler : BaseHandler, IRequestHandler<GetProfileByIdQuery, UserProfileViewModel>
{
    public GetProfileByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<UserProfileViewModel> Handle(GetProfileByIdQuery getProfileByIdQuery, CancellationToken cancellationToken) {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(getProfileByIdQuery.Id);

        if(user == null || user.IsDeleted){
            throw new KeyNotFoundException("User not found");
        }

        return _mapper.Map<UserProfileViewModel>(user);
    }
}