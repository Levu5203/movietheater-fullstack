using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Users;

public class UserGetAllQueryHandler :
    BaseHandler,
    IRequestHandler<UserGetAllQuery, IEnumerable<UserViewModel>>
{
    private readonly UserManager<User> _userManager;

    public UserGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserViewModel>> Handle(
        UserGetAllQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userManager.GetUsersInRoleAsync("Customer");
        return _mapper.Map<IEnumerable<UserViewModel>>(users);
    }
}
