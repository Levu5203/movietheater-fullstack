using MovieTheater.Business.ViewModels.Users;

namespace MovieTheater.Business.Handlers.Users;

public class CustomerSearchQuery : MasterDataSearchQuery<UserViewModel>
{
    public string? Gender { get; set; }

    public DateTime? BirthdateStart { get; set; }

    public DateTime? BirthdateEnd { get; set; }

}
