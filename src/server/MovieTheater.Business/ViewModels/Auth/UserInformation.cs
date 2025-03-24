namespace MovieTheater.Business.ViewModels.auth;

public class UserInformation
{
    public required string Id { get; set; }

    public required string DisplayName { get; set; }

    public string? Avatar { get; set; }
}