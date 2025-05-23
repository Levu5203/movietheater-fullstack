using MediatR;
using MovieTheater.Business.ViewModels.Users;

namespace MovieTheater.Business.Handlers.Users;

public class CustomerGetAllQuery : IRequest<IEnumerable<UserViewModel>>
{

}
