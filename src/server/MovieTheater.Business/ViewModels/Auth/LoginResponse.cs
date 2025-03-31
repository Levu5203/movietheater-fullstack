namespace MovieTheater.Business.ViewModels.auth;

public class LoginResponse
{
    public required string AccessToken { get; set; }

    public required string RefreshToken { get; set; }
    
    public DateTime ExpiresAt { get; set; }

    public required UserInformation User { get; set; }
}
