using MediatR;

namespace MovieTheater.Business.Handlers;

public class BaseUpdateCommand<T>: IRequest<T> where T : class
{
    public Guid Id { get; set; }
}
