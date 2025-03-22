namespace MovieTheater.Business.Handlers;

public class MasterBaseCreateCommand<T>: BaseCreateCommand<T>
{
    public bool IsActive { get; set; }
}