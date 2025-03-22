using MediatR;

namespace MovieTheater.Business.Handlers;

public class BaseGetByIdQuery<T>: IRequest<T> where T : class
{
    public Guid Id { get; set; }
}