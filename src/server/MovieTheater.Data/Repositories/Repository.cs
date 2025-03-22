using MovieTheater.Core.Constants;
using MovieTheater.Models;

namespace MovieTheater.Data.Repositories;

public class Repository<T> : RepositoryBase<T, MovieTheaterDbContext> where T : class, IBaseEntity
{
    private readonly IUserIdentity _currentUser;

    public Repository(MovieTheaterDbContext dataContext, IUserIdentity currentUser)
        : base(dataContext)
    {
        _currentUser = currentUser;
    }

    protected override Guid CurrentUserId
    {
        get
        {
            if (_currentUser != null)
            {
                return _currentUser.UserId;
            }

            return CoreConstants.SystemAdministratorId;
        }
    }

    protected override string CurrentUserName
    {
        get
        {
            if (_currentUser != null)
            {
                return _currentUser.UserName;
            }

            return CoreConstants.UserRoles.SystemAdministrator;
        }
    }
}
