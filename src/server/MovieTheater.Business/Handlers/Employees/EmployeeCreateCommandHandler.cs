using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;
namespace MovieTheater.Business.Handlers.Employees;

public class EmployeeCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IUserIdentity currentUser) :
    BaseHandler(unitOfWork, mapper),
    IRequestHandler<EmployeeCreateCommand, UserViewModel>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IUserIdentity _currentUser = currentUser;


    public async Task<UserViewModel> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {
        var currUser = await _unitOfWork.UserRepository.GetByIdAsync(_currentUser.UserId) ??
            throw new UnauthorizedAccessException($"User with ID {_currentUser.UserId} not found");

        if (await _userManager.FindByNameAsync(request.Email) != null)
        {
            throw new Exception("Email is already taken.");
        }
        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            throw new Exception("Username is already taken.");
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

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create employee: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Assign Employee role
        await _userManager.AddToRoleAsync(user, "Employee");

        return _mapper.Map<UserViewModel>(user);
    }
}


public class EmployeeUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IUserIdentity currentUser) :
    BaseHandler(unitOfWork, mapper),
    IRequestHandler<EmployeeUpdateCommand, UserViewModel>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IUserIdentity _currentUser = currentUser;

    public async Task<UserViewModel> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
    {
        var currUser = await _unitOfWork.UserRepository.GetByIdAsync(_currentUser.UserId) ??
           throw new UnauthorizedAccessException($"User with ID {_currentUser.UserId} not found");

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new KeyNotFoundException("Employee not found");
        }

        // Check if new email is already in use by another user
        var existingUserWithEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingUserWithEmail != null && existingUserWithEmail.Id != user.Id)
        {
            throw new InvalidOperationException("Email is already taken.");
        }
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber ?? null;
        user.IdentityCard = request.IdentityCard;
        user.Address = request.Address;
        user.Email = request.Email;
        user.Gender = request.Gender;
        user.DateOfBirth = request.DateOfBirth;
        user.IsActive = request.IsActive;
        user.UpdatedById = currUser.Id;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to update employee: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return _mapper.Map<UserViewModel>(user);
    }
}