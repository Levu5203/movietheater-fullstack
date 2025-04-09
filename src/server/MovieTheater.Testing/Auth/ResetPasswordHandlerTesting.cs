using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Models.Security;

namespace MovieTheater.Testing.Auth;

[TestFixture]
public class ResetPasswordCommandHandlerTests
{
    private Mock<UserManager<User>> _userManagerMock;
    private ResetPasswordCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        var store = new Mock<IUserStore<User>>();
        var options = new Mock<IOptions<IdentityOptions>>();
        var passwordHasher = new Mock<IPasswordHasher<User>>();
        var userValidators = new List<IUserValidator<User>>();
        var passwordValidators = new List<IPasswordValidator<User>>();
        var keyNormalizer = new Mock<ILookupNormalizer>();
        var errors = new IdentityErrorDescriber();
        var services = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<User>>>();

        _userManagerMock = new Mock<UserManager<User>>(
            store.Object,
            options.Object,
            passwordHasher.Object,
            userValidators,
            passwordValidators,
            keyNormalizer.Object,
            errors,
            services.Object,
            logger.Object
        );
        _handler = new ResetPasswordCommandHandler(_userManagerMock.Object);
    }

    [Test]
    public async Task Handle_UserNotFound_ReturnsFalse()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync("notfound@example.com"))
            .ReturnsAsync((User)null!);

        var command = new ResetPasswordCommand
        {
            Email = "notfound@example.com",
            Token = "some-token",
            Password = "NewPassword123!"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Handle_ResetPasswordSucceeds_ReturnsTrue()
    {
        // Arrange
        var user = new User { Email = "user@example.com", FirstName = "Test", LastName = "User", IdentityCard = "55149411459", Gender = "Male" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email))
            .ReturnsAsync(user);

        _userManagerMock.Setup(x => x.ResetPasswordAsync(user, "valid-token", "NewPassword123!"))
            .ReturnsAsync(IdentityResult.Success);

        var command = new ResetPasswordCommand
        {
            Email = user.Email,
            Token = "valid-token",
            Password = "NewPassword123!"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task Handle_ResetPasswordFails_ReturnsFalse()
    {
        // Arrange
        var user = new User { Email = "user@example.com", FirstName = "Test", LastName = "User", IdentityCard = "55149411459", Gender = "Male" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email))
            .ReturnsAsync(user);

        _userManagerMock.Setup(x => x.ResetPasswordAsync(user, "invalid-token", "NewPassword123!"))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));

        var command = new ResetPasswordCommand
        {
            Email = user.Email,
            Token = "invalid-token",
            Password = "NewPassword123!"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
    }
}
