using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using MovieTheater.Business.Handlers.Employees;
using MovieTheater.Business.Handlers.Users;
using MovieTheater.Business.Services;
using MovieTheater.Business.ViewModels.auth;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Core.Constants;
using MovieTheater.Data;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;
using NUnit.Framework;
using static MovieTheater.Core.Constants.CoreConstants;
using System.Text;

namespace MovieTheater.Testing.Employee;

public class EmployeeManagementTesting
{
    private MovieTheaterDbContext _context;

    private IMapper _mapper;

    private Mock<UserManager<User>> _userManagerMock;

    private IUnitOfWork _unitOfWork;

    private List<User> _users;

    private Mock<IUserIdentity> _userIdentityMock;

    private Mock<IAzureService> _azureServiceMock;

    private const string TestPassword = "Abc@12345";

    [SetUp]
    public async Task SetupAsync()
    {
        var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
        .UseInMemoryDatabase(databaseName: $"MovieTheaterTest_{Guid.NewGuid()}")
        .Options;

        _context = new MovieTheaterDbContext(options);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, EmployeeViewModel>();
        });
        _mapper = config.CreateMapper();

        _userManagerMock = new Mock<UserManager<User>>(
           new Mock<IUserStore<User>>().Object,
           new Mock<IOptions<IdentityOptions>>().Object,
           new PasswordHasher<User>(),
           new List<IUserValidator<User>> { new UserValidator<User>() },
           new List<IPasswordValidator<User>> { new PasswordValidator<User>() },
           new UpperInvariantLookupNormalizer(),
           new IdentityErrorDescriber(),
           new Mock<IServiceProvider>().Object,
           new Mock<ILogger<UserManager<User>>>().Object
       );
        var hasher = new PasswordHasher<User>();

        var adminUser = new User
        {
            Id = SystemAdministratorId,
            FirstName = "Super",
            LastName = "Admin",
            Gender = "Male",
            IdentityCard = "984295101801",
            Email = "admin@example.com",
            UserName = "admin@example.com",
            EmailConfirmed = true,
            IsActive = true,
            IsDeleted = false
        };
        adminUser.PasswordHash = hasher.HashPassword(adminUser, TestPassword);

        var EmployeeUser1 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Employeeone",
            LastName = "Test",
            Gender = "Male",
            IdentityCard = "984295101801",
            Email = "Employeeone@example.com",
            UserName = "Employeeone",
            EmailConfirmed = true,
            IsActive = true,
            IsDeleted = false
        };
        var EmployeeUser2 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Employeetwo",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeetwo@example.com",
            UserName = "Employeetwo",
            EmailConfirmed = true,
            IsActive = false,
            IsDeleted = false
        };
        EmployeeUser1.PasswordHash = hasher.HashPassword(EmployeeUser1, TestPassword);
        EmployeeUser2.PasswordHash = hasher.HashPassword(EmployeeUser2, TestPassword);

        _users = [adminUser, EmployeeUser1, EmployeeUser2];
        _context.Users.AddRange(_users);
        await _context.SaveChangesAsync();

        _userManagerMock.Setup(x => x.FindByIdAsync(adminUser.Id.ToString()))
            .ReturnsAsync(adminUser);
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        _userManagerMock.Setup(x => x.IsInRoleAsync(adminUser, RoleConstants.Admin))
            .ReturnsAsync(true);
        _userManagerMock.Setup(x => x.IsInRoleAsync(EmployeeUser1, RoleConstants.Admin))
            .ReturnsAsync(false);
        _userManagerMock.Setup(x => x.IsInRoleAsync(EmployeeUser2, RoleConstants.Admin))
        .ReturnsAsync(false);

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), RoleConstants.Employee))
            .ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(x => x.GetUsersInRoleAsync(RoleConstants.Employee))
            .ReturnsAsync([EmployeeUser1, EmployeeUser2]);

        _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(IdentityResult.Success);

        // Default identity mock: use Admin by default, override per test if needed
        _userIdentityMock = new Mock<IUserIdentity>();
        _userIdentityMock.Setup(x => x.UserId).Returns(adminUser.Id);

        _azureServiceMock = new Mock<IAzureService>();
        _azureServiceMock.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
            .ReturnsAsync("https://example.com/file.jpg");
        _unitOfWork = new UnitOfWork(_context, _userIdentityMock.Object);
    }
    [TearDown]
    public void TearDown()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
        _unitOfWork?.Dispose();
    }

    [Test]
    public async Task GetAllEmployees_ShouldContainEmployeeUser()
    {
        var query = new EmployeeGetAllQuery();
        var handler = new EmployeeGetAllQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(u => u.Email == "Employeeone@example.com"), Is.True);
    }

    [Test]
    public async Task DeleteEmployeeById_ValidId_ShouldReturnEmployee()
    {
        var command = new EmployeeDeleteByIdCommand { Id = _users[1].Id };
        var handler = new EmployeeDeleteByIdCommandHandler(_unitOfWork, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);
    }

    [Test]
    public void DeleteEmployeeById_InvalidId_ShouldReturnEmployee()
    {
        var command = new EmployeeDeleteByIdCommand { Id = Guid.Parse("93f8139d-3195-4a4e-647b-08dd5c670111") };
        var handler = new EmployeeDeleteByIdCommandHandler(_unitOfWork, _mapper);

        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() =>
                    handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo($"User with {command.Id} is not found"));
    }

    [Test]
    public async Task UpdateEmployeeStatus_ValidId_ShouldReturnEmployee()
    {
        var command = new EmployeeUpdateStatusCommand { Id = _users[1].Id };
        var handler = new EmployeeUpdateStatusCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsActive, Is.False);
    }

    [Test]
    public void UpdateEmployeeStatus_InvalidId_ShouldReturnEmployee()
    {
        var command = new EmployeeUpdateStatusCommand { Id = Guid.Parse("93f8139d-3195-4a4e-647b-08dd5c670111") };
        var handler = new EmployeeUpdateStatusCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object);

        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() =>
                     handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo($"Employee with {command.Id} is not found"));
    }

    [Test]
    public async Task SearchEmployee_WithNoKeyword_ShouldReturnAllEmployees()
    {
        var query = new EmployeeSearchQuery { IncludeInactive = true };
        var handler = new EmployeeSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(2));
        });
        Assert.That(result.Items, Has.Length.EqualTo(2));
    }

    [Test]
    public async Task SearchEmployee_WithKeyword_ShouldReturnFilteredEmployees()
    {
        var query = new EmployeeSearchQuery { Keyword = "Employee", IncludeInactive = true };
        var handler = new EmployeeSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(2));
        });
        Assert.That(result.Items, Has.Length.EqualTo(2));
    }

    [Test]
    public async Task SearchEmployee_WithKeywordAndGender_ShouldReturnFilteredEmployees()
    {
        var query = new EmployeeSearchQuery { Keyword = "Employee", Gender = "Male", IncludeInactive = true };
        var handler = new EmployeeSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(1));
        });
        Assert.That(result.Items, Has.Length.EqualTo(1));
    }

    [Test]
    public async Task SearchEmployee_WithKeywordAndGenderAndStatus_ShouldReturnFilteredEmployees()
    {
        var query = new EmployeeSearchQuery { Keyword = "Employee", Gender = "Male", IsActive = true, IncludeInactive = true };
        var handler = new EmployeeSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(1));
        });
        Assert.That(result.Items, Has.Length.EqualTo(1));
    }

    [Test]
    public async Task SearchEmployee_WithInvalidKeyword_ShouldReturnNoEmployees()
    {
        var query = new EmployeeSearchQuery { Keyword = "invalid", IncludeInactive = true };
        var handler = new EmployeeSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task CreateEmployee_WithoutAvatar()
    {
        var command = new EmployeeCreateCommand
        {
            FirstName = "Employeethree",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeethree@example.com",
            Username = "Employeethree",
            IsActive = false,
        };
        var handler = new EmployeeCreateCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object, _azureServiceMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.Username, Is.EqualTo(command.Username));
            Assert.That(result.Email, Is.EqualTo(command.Email));
        });
    }

    [Test]
    public async Task CreateEmployee_WithAvatar()
    {
        var fileContent = "Fake image data";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
        var avatar = new FormFile(stream, 0, stream.Length, "avatar", "avatar.png")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };
        var command = new EmployeeCreateCommand
        {
            FirstName = "Employeethree",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeethree@example.com",
            Username = "Employeethree",
            Avatar = avatar,
            IsActive = false,
        };
        var handler = new EmployeeCreateCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object, _azureServiceMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.Username, Is.EqualTo(command.Username));
            Assert.That(result.Email, Is.EqualTo(command.Email));
            Assert.That(result.Avatar, Is.EqualTo("https://example.com/file.jpg"));
        });
    }

    [Test]
    public void CreateEmployee_WithExistedEmail()
    {
        var command = new EmployeeCreateCommand
        {
            FirstName = "Employeethree",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeethree@example.com",
            Username = "Employeetwo",
            IsActive = false,
        };
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new User
            {
                UserName = command.Username,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender,
                IdentityCard = command.IdentityCard
            });
        var handler = new EmployeeCreateCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object, _azureServiceMock.Object);
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                     handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Email already existed."));
    }

    [Test]
    public void CreateEmployee_WithExistedUsername()
    {
        var command = new EmployeeCreateCommand
        {
            FirstName = "Employeethree",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeethree@example.com",
            Username = "Employeetwo",
            IsActive = false,
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
        var handler = new EmployeeCreateCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object, _azureServiceMock.Object);
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                     handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Username already existed."));
    }

    [Test]
    public async Task UpdateEmployee_WithoutAvatar()
    {
        var command = new EmployeeUpdateCommand
        {
            Id = _users[1].Id,
            FirstName = "Employeethree",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeethree@example.com",
            IsActive = false,
        };
        var handler = new EmployeeUpdateCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object, _azureServiceMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.Email, Is.EqualTo(command.Email));
            Assert.That(result.Avatar, Is.Null);
        });
    }

    [Test]
    public async Task UpdateEmployee_WithAvatar()
    {
        var fileContent = "Fake image data";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
        var avatar = new FormFile(stream, 0, stream.Length, "avatar", "avatar.png")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };
        var command = new EmployeeUpdateCommand
        {
            Id = _users[1].Id,
            FirstName = "Employeethree",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeethree@example.com",
            Avatar = avatar,
            IsActive = false,
        };
        var handler = new EmployeeUpdateCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object, _azureServiceMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo(command.FirstName));
            Assert.That(result.LastName, Is.EqualTo(command.LastName));
            Assert.That(result.Email, Is.EqualTo(command.Email));
            Assert.That(result.Avatar, Is.EqualTo("https://example.com/file.jpg"));
        });
    }

    [Test]
    public void UpdateEmployee_WithExistedEmail()
    {
        var command = new EmployeeUpdateCommand
        {
            Id = _users[1].Id,
            FirstName = "Employeethree",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "Employeethree@example.com",
            IsActive = false,
        };
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender,
                IdentityCard = command.IdentityCard
            });
        var handler = new EmployeeUpdateCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object, _azureServiceMock.Object);
        var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                     handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo("Email is already taken."));
    }
}
