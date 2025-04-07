using MovieTheater.Business.ViewModels.Users;

namespace MovieTheater.Business.Handlers.Customers;

public class CustomerSearchQuery : MasterDataSearchQuery<UserViewModel>
{
    public string? Gender { get; set; }

    public bool? IsActive { get; set; }
}
