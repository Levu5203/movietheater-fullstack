using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Business.Services;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;
namespace MovieTheater.Business.Handlers.Employees;

public class EmployeeCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IUserIdentity currentUser, IAzureService azureService) :
    BaseHandler(unitOfWork, mapper),
    IRequestHandler<EmployeeCreateCommand, EmployeeViewModel>
{
    private readonly UserManager<User> _userManager = userManager;

    private readonly IUserIdentity _currentUser = currentUser;

    private readonly IAzureService _azureService = azureService;

    public async Task<EmployeeViewModel> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {
        var currUser = await _unitOfWork.UserRepository.GetByIdAsync(_currentUser.UserId) ??
            throw new UnauthorizedAccessException($"User with ID {_currentUser.UserId} not found");

        if (await _userManager.FindByNameAsync(request.Email) != null)
        {
            throw new InvalidOperationException("Email is already taken.");
        }
        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            throw new InvalidOperationException("Username is already taken.");
        }

        var user = new User
        {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber ?? null,
            Gender = request.Gender,
            IdentityCard = request.IdentityCard,
            DateOfBirth = request.DateOfBirth ?? null,
            Address = request.Address ?? null,
            IsActive = true,
            EmailConfirmed = true,
            CreatedById = currUser.Id
        };
        if (request.Avatar != null && request.Avatar.Length > 0)
        {
            var fileName = $"/{Guid.NewGuid()}_{request.Avatar.FileName}";
            var avatarUrl = await _azureService.UploadFileAsync(request.Avatar, fileName);
            user.Avatar = avatarUrl;
        }

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to create employee: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Assign Employee role
        await _userManager.AddToRoleAsync(user, "Employee");

        return _mapper.Map<EmployeeViewModel>(user);
    }
}
