using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MovieTheater.Business.Handlers.Customers;
using MovieTheater.Business.Handlers.Users;
using MovieTheater.Business.ViewModels.auth;
using MovieTheater.Business.ViewModels.Users;
using MovieTheater.Core.Constants;
using MovieTheater.Data;
using MovieTheater.Data.Repositories;
using MovieTheater.Data.UnitOfWorks;
using MovieTheater.Models.Security;
using NUnit.Framework;
using static MovieTheater.Core.Constants.CoreConstants;

namespace MovieTheater.Testing.Customer;

public class CustomerManagementTesting
{
    private MovieTheaterDbContext _context;

    private IMapper _mapper;

    private Mock<UserManager<User>> _userManagerMock;

    private IUnitOfWork _unitOfWork;

    private List<User> _users;

    private Mock<IUserIdentity> _userIdentityMock;
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
            cfg.CreateMap<User, UserViewModel>();
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

        var customerUser1 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Customerone",
            LastName = "Test",
            Gender = "Male",
            IdentityCard = "984295101801",
            Email = "customerone@example.com",
            UserName = "customerone@example.com",
            EmailConfirmed = true,
            IsActive = true,
            IsDeleted = false
        };
        var customerUser2 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Customertwo",
            LastName = "Test",
            Gender = "Female",
            IdentityCard = "984295101801",
            Email = "customertwo@example.com",
            UserName = "customertwo@example.com",
            EmailConfirmed = true,
            IsActive = false,
            IsDeleted = false
        };
        customerUser1.PasswordHash = hasher.HashPassword(customerUser1, TestPassword);
        customerUser2.PasswordHash = hasher.HashPassword(customerUser2, TestPassword);

        _users = [adminUser, customerUser1, customerUser2];
        _context.Users.AddRange(_users);
        await _context.SaveChangesAsync();

        _userManagerMock.Setup(x => x.FindByIdAsync(adminUser.Id.ToString()))
            .ReturnsAsync(adminUser);
        _userManagerMock.Setup(x => x.FindByIdAsync(customerUser1.Id.ToString()))
            .ReturnsAsync(customerUser1);
        _userManagerMock.Setup(x => x.FindByIdAsync(customerUser2.Id.ToString()))
            .ReturnsAsync(customerUser2);

        _userManagerMock.Setup(x => x.IsInRoleAsync(adminUser, RoleConstants.Admin))
            .ReturnsAsync(true);
        _userManagerMock.Setup(x => x.IsInRoleAsync(customerUser1, RoleConstants.Admin))
            .ReturnsAsync(false);
        _userManagerMock.Setup(x => x.IsInRoleAsync(customerUser2, RoleConstants.Admin))
        .ReturnsAsync(false);

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), RoleConstants.Customer))
            .ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(x => x.GetUsersInRoleAsync(RoleConstants.Customer))
            .ReturnsAsync([customerUser1, customerUser2]);

        _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(IdentityResult.Success);

        // Default identity mock: use Admin by default, override per test if needed
        _userIdentityMock = new Mock<IUserIdentity>();
        _userIdentityMock.Setup(x => x.UserId).Returns(adminUser.Id);

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
    public async Task GetAllCustomers_ShouldContainCustomerUser()
    {
        var query = new CustomerGetAllQuery();
        var handler = new CustomerGetAllQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(u => u.Email == "customerone@example.com"), Is.True);
    }

    [Test]
    public async Task DeleteCustomerById_ValidId_ShouldReturnCustomer()
    {
        var command = new CustomerDeleteByIdCommand { Id = _users[1].Id };
        var handler = new CustomerDeleteCommandHandler(_unitOfWork, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);
    }

    [Test]
    public void DeleteCustomerById_InvalidId_ShouldReturnCustomer()
    {
        var command = new CustomerDeleteByIdCommand { Id = Guid.Parse("93f8139d-3195-4a4e-647b-08dd5c670111") };
        var handler = new CustomerDeleteCommandHandler(_unitOfWork, _mapper);

        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() =>
                    handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo($"User with {command.Id} is not found"));
    }

    [Test]
    public async Task UpdateCustomerStatus_ValidId_ShouldReturnCustomer()
    {
        var command = new CustomerUpdateStatusCommand { Id = _users[1].Id };
        var handler = new CustomerUpdateStatusCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsActive, Is.False);
    }

    [Test]
    public void UpdateCustomerStatus_InvalidId_ShouldReturnCustomer()
    {
        var command = new CustomerUpdateStatusCommand { Id = Guid.Parse("93f8139d-3195-4a4e-647b-08dd5c670111") };
        var handler = new CustomerUpdateStatusCommandHandler(_unitOfWork, _mapper, _userManagerMock.Object, _userIdentityMock.Object);

        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() =>
                     handler.Handle(command, CancellationToken.None));
        Assert.That(ex.Message, Is.EqualTo($"User with {command.Id} is not found"));
    }

    [Test]
    public async Task SearchCustomer_WithNoKeyword_ShouldReturnAllCustomers()
    {
        var query = new CustomerSearchQuery { IncludeInactive = true };
        var handler = new CustomerSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(2));
        });
        Assert.That(result.Items, Has.Length.EqualTo(2));
    }

    [Test]
    public async Task SearchCustomer_WithKeyword_ShouldReturnFilteredCustomers()
    {
        var query = new CustomerSearchQuery { Keyword = "customer", IncludeInactive = true };
        var handler = new CustomerSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(2));
        });
        Assert.That(result.Items, Has.Length.EqualTo(2));
    }

    [Test]
    public async Task SearchCustomer_WithKeywordAndGender_ShouldReturnFilteredCustomers()
    {
        var query = new CustomerSearchQuery { Keyword = "customer", Gender = "Male", IncludeInactive = true };
        var handler = new CustomerSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(1));
        });
        Assert.That(result.Items, Has.Length.EqualTo(1));
    }

    [Test]
    public async Task SearchCustomer_WithKeywordAndGenderAndStatus_ShouldReturnFilteredCustomers()
    {
        var query = new CustomerSearchQuery { Keyword = "customer", Gender = "Male", IsActive = true, IncludeInactive = true };
        var handler = new CustomerSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Not.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(1));
        });
        Assert.That(result.Items, Has.Length.EqualTo(1));
    }

    [Test]
    public async Task SearchCustomer_WithInvalidKeyword_ShouldReturnNoCustomers()
    {
        var query = new CustomerSearchQuery { Keyword = "invalid", IncludeInactive = true };
        var handler = new CustomerSearchQueryHandler(_unitOfWork, _mapper, _userManagerMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Items, Is.Empty);
            Assert.That(result.TotalCount, Is.EqualTo(0));
        });
    }
}
