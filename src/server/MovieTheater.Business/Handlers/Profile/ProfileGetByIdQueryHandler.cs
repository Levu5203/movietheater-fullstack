using AutoMapper;
using MediatR;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Profile;

public class ProfileGetByIdQueryHandler : BaseHandler, IRequestHandler<ProfileGetByIdQuery, UserProfileViewModel>
{
    public ProfileGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<UserProfileViewModel> Handle(ProfileGetByIdQuery request, CancellationToken cancellationToken) {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

        if(user == null || user.IsDeleted){
            throw new KeyNotFoundException("User not found");
        }

        return _mapper.Map<UserProfileViewModel>(user);
    }
}