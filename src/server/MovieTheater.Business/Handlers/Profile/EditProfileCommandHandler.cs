using AutoMapper;
using MediatR;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Profile;

public class EditProfileCommandHandle : BaseHandler, IRequestHandler<EditProfileCommand, UserProfileViewModel>
{
    public EditProfileCommandHandle(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<UserProfileViewModel> Handle(EditProfileCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

            if (user == null || user.IsDeleted)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
            user.Gender = request.Gender;
            user.IdentityCard = request.IdentityCard;
            user.Avatar = request.Avatar;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            var result = _mapper.Map<UserProfileViewModel>(user);

            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
