using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Users;

public class CustomerGetAllQueryHandler :
    BaseHandler,
    IRequestHandler<CustomerGetAllQuery, IEnumerable<UserViewModel>>
{
    private readonly UserManager<User> _userManager;

    public CustomerGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserViewModel>> Handle(
        CustomerGetAllQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userManager.GetUsersInRoleAsync("Customer");
        return _mapper.Map<IEnumerable<UserViewModel>>(users);
    }
}
