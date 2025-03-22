using MediatR;

namespace MovieTheater.Business.Handlers;

public class BaseGetAllQuery<T> : 
    IRequest<IEnumerable<T>> where T : class
{
}