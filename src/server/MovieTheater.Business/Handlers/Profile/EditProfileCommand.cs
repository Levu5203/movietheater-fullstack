using Microsoft.AspNetCore.Http;
using MovieTheater.Business.ViewModels.Profile;

namespace MovieTheater.Business.Handlers.Profile;

public class EditProfileCommand : BaseUpdateCommand<UserProfileViewModel>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public required string Gender { get; set; }
    public required string IdentityCard { get; set; }
    public IFormFile? Avatar { get; set; }
    public string? PhoneNumber { get; set; }
}
