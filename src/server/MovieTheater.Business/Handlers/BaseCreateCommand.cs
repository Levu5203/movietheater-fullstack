using MediatR;

namespace MovieTheater.Business.Handlers;

public class BaseCreateCommand<T>: IRequest<T>
{
    public Guid? Id { get; set; }
}

public class BaseCreateUpdateCommand<T>: IRequest<T>
{
    public Guid? Id { get; set; }
}