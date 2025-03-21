using MovieTheater.Core.Constants;
using MovieTheater.Models;

namespace MovieTheater.Data.Repositories;

public class MasterDataRepository<T> : MasterDataRepositoryBase<T, MovieTheaterDbContext> where T : class,
        IMasterDataBaseEntity
{
    private readonly IUserIdentity _currentUser;

    public MasterDataRepository(MovieTheaterDbContext dataContext, IUserIdentity currentUser)
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
