using System.Linq.Expressions;
using MovieTheater.Models;

namespace MovieTheater.Data.Repositories;

public interface IMasterDataRepository<T> : IRepository<T> where T : class, IMasterDataBaseEntity
{
    IEnumerable<T> GetAllWithInactive();

    IEnumerable<T> GetManyWithInactive(Expression<Func<T, bool>> where);

    IQueryable<T> GetQuery(bool includeInactive = false);

    new IQueryable<T> GetQuery(Expression<Func<T, bool>> where);

    IQueryable<T> GetQueryWithInactive(Expression<Func<T, bool>> where);

    IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
}
