using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MovieTheater.Business.Handlers.Auth;
using MovieTheater.Business.Services;
using MovieTheater.Business.ViewModels.Profile;
using MovieTheater.Data;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;

namespace MovieTheater.Testing.Auth;

[TestFixture]
public class LoginHandlerTesting
{
    private MovieTheaterDbContext _dbContext;
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private List<User> _users;
    private string _databaseName;
    private Mock<IUserIdentity> _userIdentityMock;
    private UserManager<User> _userManager;
    private Mock<SignInManager<User>> _signInManagerMock;
    private Mock<ITokenService> _tokenServiceMock;
    private IConfiguration _configuration;

    private LoginRequestCommandHandler _handler;
    private const string TestPassword = "Abc@12345";

    [SetUp]
    public async Task Setup()
    {
        // Create a unique database name for each test to ensure isolation
        _databaseName = $"MovieTheaterTest_{Guid.NewGuid()}";

        // Setup the in-memory database
        var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
            .UseInMemoryDatabase(_databaseName)
            .Options;

        _dbContext = new MovieTheaterDbContext(options);

        // Setup AutoMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserProfileViewModel>();
        });
        _mapper = mapperConfig.CreateMapper();

        // Create a mock user store that implements all required interfaces
        var userStoreMock = new Mock<IUserEmailStore<User>>();
        userStoreMock.As<IUserPasswordStore<User>>();
        userStoreMock.As<IUserRoleStore<User>>();
        userStoreMock.As<IUserRoleStore<User>>()
                .Setup(x => x.GetRolesAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);
        userStoreMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken token) =>
            {
                // Return the matching user or null if not found
                return _users.FirstOrDefault(u => u.Email!.Equals(email, StringComparison.OrdinalIgnoreCase));
            });

        // Setup UserManager
        _userManager = new UserManager<User>(
            userStoreMock.Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new PasswordHasher<User>(),
            [new UserValidator<User>()],
            [new PasswordValidator<User>()],
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<User>>>().Object
        );

        // Setup SignInManager mock
        _signInManagerMock = new Mock<SignInManager<User>>(
            _userManager,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<User>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<ILogger<SignInManager<User>>>(),
            Mock.Of<IAuthenticationSchemeProvider>(),
            Mock.Of<IUserConfirmation<User>>()
        );

        // Setup test users with pre-hashed passwords
        var hasher = new PasswordHasher<User>();
        _users = [
            new() {
                Id = Guid.NewGuid(),
                UserName = "user1",
                FirstName = "Customer",
                LastName = "one",
                Gender = "Male",
                IdentityCard = "389518903518",
                Email = "user1@example.com",
                IsDeleted = false,
                IsActive = true,
                EmailConfirmed = true,
            },
            new() {
                Id = Guid.NewGuid(),
                UserName = "user2",
                FirstName = "Customer",
                LastName = "two",
                Gender = "Male",
                IdentityCard = "389518903518",
                Email = "user2@example.com",
                IsDeleted = false,
                IsActive = false,
                EmailConfirmed = true,
            },
        ];
        _users[0].PasswordHash = hasher.HashPassword(_users[0], TestPassword);
        _users[1].PasswordHash = hasher.HashPassword(_users[1], TestPassword);
        // Add test users to the in-memory database
        _dbContext.Users.AddRange(_users);
        await _dbContext.SaveChangesAsync();

        // Setup UserStore mock behavior
        _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(
               It.IsAny<User>(),
               It.IsAny<string>(),
               It.IsAny<bool>()))
           .ReturnsAsync((User user, string password, bool lockoutOnFailure) =>
           {
               if (user == null)
                   return SignInResult.Failed;

               var result = hasher.VerifyHashedPassword(user, user.PasswordHash!, password);
               return result == PasswordVerificationResult.Success
                   ? SignInResult.Success
                   : SignInResult.Failed;
           });

        // Setup token service mock
        _tokenServiceMock = new Mock<ITokenService>();
        _tokenServiceMock.Setup(x => x.GenerateTokenAsync(It.IsAny<User>(), It.IsAny<IList<string>>()))
            .ReturnsAsync("mock-access-token");
        _tokenServiceMock.Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new RefreshToken
            {
                Token = "mock-refresh-token",
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            });

        // Setup configuration mock
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["JWT:Secret"]).Returns("very-secret-key-with-at-least-32-characters");
        configurationMock.Setup(c => c["JWT:ValidIssuer"]).Returns("MovieTheater");
        configurationMock.Setup(c => c["JWT:ValidAudience"]).Returns("MovieTheater-Client");
        _configuration = configurationMock.Object;

        _userIdentityMock = new Mock<IUserIdentity>();
        _userIdentityMock.Setup(ui => ui.UserId).Returns(_users[0].Id);
        _userIdentityMock.Setup(ui => ui.UserName).Returns(_users[0].UserName!);

        _unitOfWork = new UnitOfWork(_dbContext, _userIdentityMock.Object);
        _handler = new LoginRequestCommandHandler(_unitOfWork, _mapper, _userManager, _signInManagerMock.Object, _tokenServiceMock.Object, _configuration);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext?.Database.EnsureDeleted();
        _userManager?.Dispose();
        _dbContext?.Dispose();
        _unitOfWork?.Dispose();
    }


    [Test]
    public async Task Login_WithValidInputs_ReturnLoginResponse()
    {
        var user = _users[0];
        var loginCommand = new LoginRequestCommand
        {
            Email = "user1@example.com",
            Password = TestPassword
        };

        var result = await _handler.Handle(loginCommand, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.AccessToken, Is.EqualTo("mock-access-token"));
            Assert.That(result.RefreshToken, Is.EqualTo("mock-refresh-token"));
            Assert.That(result.User, Is.Not.Null);
            Assert.That(result.User.Id, Is.EqualTo(user.Id.ToString()));
            Assert.That(result.User.DisplayName, Is.EqualTo($"{user.FirstName} {user.LastName}"));
        });
    }

    [Test]
    public async Task Login_WithInvalidEmail_ReturnsError()
    {
        var loginCommand = new LoginRequestCommand
        {
            Email = "nonexistent@example.com",
            Password = TestPassword
        };

        var ex = await Task.Run(() => Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
           _handler.Handle(loginCommand, CancellationToken.None)));
        Assert.That(ex!.Message, Is.EqualTo("The email address you entered isn't connected to an account."));
    }

    [Test]
    public async Task Login_WithWrongPassword_ReturnsError()
    {
        var loginCommand = new LoginRequestCommand
        {
            Email = "user1@example.com",
            Password = "WrongPassword123"
        };

        var ex =await Task.Run(() =>  Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
           _handler.Handle(loginCommand, CancellationToken.None)));
        Assert.That(ex!.Message, Is.EqualTo("Invalid password. Try other password for account with email user1@example.com"));
    }

    [Test]
    public async Task Login_WithInactiveUser_ReturnsError()
    {
        var loginCommand = new LoginRequestCommand
        {
            Email = "user2@example.com",
            Password = TestPassword
        };

        var ex = await Task.Run(() => Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
           _handler.Handle(loginCommand, CancellationToken.None)));
        Assert.That(ex!.Message, Is.EqualTo("Your account is deactivated. Please contact an administrator."));
    }
}
