using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Business.Handlers.Employees;

public class EmployeeGetAllQueryHandler :
    BaseHandler,
    IRequestHandler<EmployeeGetAllQuery, IEnumerable<EmployeeViewModel>>
{
    private readonly UserManager<User> _userManager;

    public EmployeeGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<EmployeeViewModel>> Handle(
        EmployeeGetAllQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userManager.GetUsersInRoleAsync("Employee");
        return _mapper.Map<IEnumerable<EmployeeViewModel>>(users);
    }
}
