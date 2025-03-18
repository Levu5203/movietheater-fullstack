namespace MovieTheater.Models;

public interface IMasterDataBaseEntity : IBaseEntity
{
    public bool IsActive { get; set; }
}