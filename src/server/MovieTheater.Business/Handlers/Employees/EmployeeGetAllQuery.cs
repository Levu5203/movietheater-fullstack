using MediatR;
using MovieTheater.Business.ViewModels.Users;

namespace MovieTheater.Business.Handlers.Employees;

public class EmployeeGetAllQuery : IRequest<IEnumerable<UserViewModel>>
{

}
