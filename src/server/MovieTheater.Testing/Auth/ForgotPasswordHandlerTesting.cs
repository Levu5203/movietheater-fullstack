using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Business.Services;
using MovieTheater.Models.Security;

namespace MovieTheater.Testing.Handlers.Auth;

[TestFixture]
public class ForgotPasswordCommandHandlerTests
{
    private Mock<UserManager<User>> _userManagerMock;
    private Mock<IEmailService> _emailServiceMock;
    private Mock<IConfiguration> _configurationMock;
    private ForgotPasswordCommandHandler _handler;
    private User _testUser;

    [SetUp]
    public void Setup()
    {
        // Setup UserManager mock
        var store = new Mock<IUserStore<User>>();
        store.As<IUserEmailStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(
            store.Object, null, null, null, null, null, null, null, null);

        // Setup test user
        _testUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "user@example.com",
            UserName = "testuser",
            EmailConfirmed = true,
            FirstName = "testuser",
            LastName = "testuser",
            PasswordHash = new PasswordHasher<User>().HashPassword(null, "testpassword"),
            IsActive = true,
            IsDeleted = false,
            Gender = "male",
            IdentityCard = "123456789"
        };

        // Setup EmailService mock
        _emailServiceMock = new Mock<IEmailService>();

        // Setup Configuration mock
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["FrontendSettings:BaseUrl"])
            .Returns("https://example.com");

        // Create handler instance
        _handler = new ForgotPasswordCommandHandler(
            _userManagerMock.Object,
            _emailServiceMock.Object,
            _configurationMock.Object);
    }

    [Test]
    public async Task Handle_WithExistingEmail_ReturnsTrueAndSendsEmail()
    {
        // Arrange
        const string testToken = "test-reset-token";

        _userManagerMock.Setup(x => x.FindByEmailAsync("user@example.com"))
            .ReturnsAsync(_testUser);

        _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(_testUser))
            .ReturnsAsync(testToken);

        var command = new ForgotPasswordCommand { Email = "user@example.com" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);

        // Verify
        _userManagerMock.Verify(x => x.GeneratePasswordResetTokenAsync(_testUser), Times.Once);

        _emailServiceMock.Verify(x => x.SendEmailAsync(
            "user@example.com",
            "Reset Password",
            It.Is<string>(body =>
                body.Contains("https://example.com/reset-password") &&
                body.Contains(testToken))));
    }

    [Test]
    public async Task Handle_WithNonExistingEmail_ReturnsFalse()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync("nonexistent@example.com"))
            .ReturnsAsync((User)null);

        var command = new ForgotPasswordCommand { Email = "nonexistent@example.com" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);

        // Verify
        _userManagerMock.Verify(x => x.GeneratePasswordResetTokenAsync(It.IsAny<User>()), Times.Never);
        _emailServiceMock.Verify(x => x.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never);
    }

    [Test]
    public async Task Handle_ProperlyFormatsResetLink()
    {
        // Arrange
        const string testToken = "test-token-123";
        _configurationMock.Setup(c => c["FrontendSettings:BaseUrl"])
            .Returns("https://myapp.com");

        _userManagerMock.Setup(x => x.FindByEmailAsync("user@example.com"))
            .ReturnsAsync(_testUser);

        _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(_testUser))
            .ReturnsAsync(testToken);

        var command = new ForgotPasswordCommand { Email = "user@example.com" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);

        // Verify link format
        _emailServiceMock.Verify(x => x.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.Is<string>(body =>
                body.Contains("https://myapp.com/reset-password?email=user@example.com&token=test-token-123"))),
            Times.Once);
    }
}