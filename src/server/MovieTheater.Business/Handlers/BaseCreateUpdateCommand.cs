using MediatR;

namespace MovieTheater.Business.Handlers;

public class BaseCreateUpdateCommand<T>: IRequest<T>
{
    public Guid? Id { get; set; }
}