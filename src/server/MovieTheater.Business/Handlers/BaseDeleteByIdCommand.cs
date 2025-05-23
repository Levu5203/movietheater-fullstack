using MediatR;

namespace MovieTheater.Business.Handlers;

public class BaseDeleteByIdCommand<T> : IRequest<T>
{
    public Guid Id { get; set; }

    public bool IsHardDelete { get; set; } = false;
}