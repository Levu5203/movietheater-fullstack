namespace MovieTheater.Data.DataSeeding;

internal class UserJsonViewModel
{
    public Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string UserName { get; set; }

    public required string Address { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string IdentityCard { get; set; }

    public required string Gender { get; set; }

    public required int TotalScore { get; set; }

    public required string PhoneNumber { get; set; }

    public required string DateOfBirth { get; set; }

    public required string Role { get; set; }
}
