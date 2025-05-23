using MovieTheater.Business.ViewModels.Users;

namespace MovieTheater.Business.Handlers.Employees;

public class EmployeeSearchQuery : MasterDataSearchQuery<EmployeeViewModel>
{
    public string? Gender { get; set; }

    public bool? IsActive { get; set; }
}
