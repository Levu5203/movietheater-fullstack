using AutoMapper;
using MediatR;
using MovieTheater.Business.Services;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Data.UnitOfWorks;

namespace MovieTheater.Business.Handlers.Profile;

public class EditProfileCommandHandle(IUnitOfWork unitOfWork, IMapper mapper, IAzureService azureService) : BaseHandler(unitOfWork, mapper), IRequestHandler<EditProfileCommand, UserProfileViewModel>
{
    private readonly IAzureService _azureService = azureService;
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

            if (request.Avatar != null && request.Avatar.Length > 0)
            {
                var fileName = $"/{Guid.NewGuid()}_{request.Avatar.FileName}";
                var avatarUrl = await _azureService.UploadFileAsync(request.Avatar, fileName);
                user.Avatar = avatarUrl;
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
            user.Gender = request.Gender;
            user.IdentityCard = request.IdentityCard;
            user.PhoneNumber = request.PhoneNumber ?? null;
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
