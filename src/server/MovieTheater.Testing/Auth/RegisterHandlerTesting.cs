using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Business.Services;
using MovieTheater.Business.ViewModels.auth;
using MovieTheater.Data;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;
using static MovieTheater.Core.Constants.CoreConstants;

namespace MovieTheater.Testing.Auth;

[TestFixture]
public class RegisterRequestCommandHandlerTests
{
    private MovieTheaterDbContext _dbContext;
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private Mock<UserManager<User>> _userManagerMock;
    private Mock<ITokenService> _tokenServiceMock;
    private Mock<IConfiguration> _configurationMock;
    private RegisterRequestCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
            .UseInMemoryDatabase(databaseName: $"MovieTheaterTest_{Guid.NewGuid()}")
            .Options;
        _dbContext = new MovieTheaterDbContext(options);

        // Setup AutoMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserInformation>();
        });
        _mapper = mapperConfig.CreateMapper();

        // Setup UserManager mock with all required interfaces
        var store = new Mock<IUserStore<User>>();
        store.As<IUserEmailStore<User>>();
        store.As<IUserPasswordStore<User>>();
        store.As<IUserRoleStore<User>>();

        _userManagerMock = new Mock<UserManager<User>>(
            store.Object,
            Mock.Of<IOptions<IdentityOptions>>(),
            new PasswordHasher<User>(),
            new List<IUserValidator<User>> { new UserValidator<User>() },
            new List<IPasswordValidator<User>> { new PasswordValidator<User>() },
            Mock.Of<ILookupNormalizer>(),
            new IdentityErrorDescriber(),
            Mock.Of<IServiceProvider>(),
            Mock.Of<ILogger<UserManager<User>>>()
        );

        // Setup default mock behaviors
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), RoleConstants.Customer))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync([RoleConstants.Customer]);

        // Setup TokenService mock
        _tokenServiceMock = new Mock<ITokenService>();
        _tokenServiceMock.Setup(x => x.GenerateTokenAsync(It.IsAny<User>(), It.IsAny<IList<string>>()))
            .ReturnsAsync("mock-access-token");
        _tokenServiceMock.Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new RefreshToken { Token = "mock-refresh-token" });

        // Setup Configuration mock
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(x => x["JWT:AccessTokenExpiryMinutes"]).Returns("15");

        _unitOfWork = new UnitOfWork(_dbContext, Mock.Of<IUserIdentity>());
        _handler = new RegisterRequestCommandHandler(
            _unitOfWork,
            _mapper,
            _userManagerMock.Object,
            _tokenServiceMock.Object,
            _configurationMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _unitOfWork.Dispose();
        _dbContext.Dispose();
    }

    [Test]
    public async Task Handle_WithValidInputs_ReturnsLoginResponse()
    {
        var command = new RegisterRequestCommand
        {
            Username = "newuser",
            Email = "new@example.com",
            Password = "Valid@Password123",
            ConfirmPassword = "Valid@Password123",
            FirstName = "Test",
            LastName = "User",
            Gender = "Male",
            IdentityCard = "123456789"
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.AccessToken, Is.EqualTo("mock-access-token"));
            Assert.That(result.RefreshToken, Is.EqualTo("mock-refresh-token"));
            Assert.That(result.User, Is.Not.Null);
            Assert.That(result.User.DisplayName, Is.EqualTo("Test User"));
        });

        // Verify
        _userManagerMock.Verify(
            x => x.CreateAsync(
                It.Is<User>(u =>
                    u.UserName == command.Username &&
                    u.Email == command.Email &&
                    u.FirstName == command.FirstName &&
                    u.LastName == command.LastName),
                command.Password),
            Times.Once);

        _userManagerMock.Verify(
            x => x.AddToRoleAsync(
                It.Is<User>(u => u.UserName == command.Username),
                RoleConstants.Customer),
            Times.Once);

        _tokenServiceMock.Verify(
            x => x.GenerateTokenAsync(
                It.Is<User>(u => u.UserName == command.Username),
                It.IsAny<IList<string>>()),
            Times.Once);
    }

    [Test]
    public void Handle_WithExistingUsername_ThrowsException()
    {
        var command = new RegisterRequestCommand
        {
            Username = "existinguser",
            Email = "new@example.com",
            Password = "Valid@Password123",
            FirstName = "Test",
            LastName = "User",
            Gender = "Male",
            IdentityCard = "123456789",
            ConfirmPassword = "Valid@Password123"
        };

        _userManagerMock.Setup(x => x.FindByNameAsync(command.Username))
            .ReturnsAsync(new User
            {
                UserName = command.Username,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender,
                IdentityCard = command.IdentityCard
            });

        var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Username already exists"));
    }

    [Test]
    public void Handle_WithExistingEmail_ThrowsException()
    {
        var command = new RegisterRequestCommand
        {
            Username = "newuser",
            Email = "existing@example.com",
            Password = "Valid@Password123",
            FirstName = "Test",
            LastName = "User",
            Gender = "Male",
            IdentityCard = "123456789",
            ConfirmPassword = "Valid@Password123"
        };

        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new User
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender,
                IdentityCard = command.IdentityCard
            });

        var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Email already exists"));
    }

    [Test]
    public void Handle_WithWeakPassword_ThrowsException()
    {
        var command = new RegisterRequestCommand
        {
            Username = "newuser",
            Email = "new@example.com",
            Password = "weak",
            FirstName = "Test",
            LastName = "User",
            Gender = "Male",
            IdentityCard = "123456789",
            ConfirmPassword = "Valid@Password123"
        };

        var errors = new[] { new IdentityError { Description = "Password too weak" } };
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), command.Password))
            .ReturnsAsync(IdentityResult.Failed(errors));

        var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Does.Contain("Failed to create user"));
        Assert.That(ex.Message, Does.Contain("Password too weak"));
    }
}