using System.Linq.Expressions;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task Add(T obj, bool commit = true);

        Task<T?> Get(int id, string includeProperties = "");

        Task<T?> Get(Expression<Func<T, bool>> filter, string includeProperties = "");

        Task<List<T>> Get(string? includeProperties = "");

        Task Delete(T obj, bool commit = true);

        Task Update(T obj, bool commit = true);

        Task Commit();

        IEnumerable<T> Find(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

        IQueryable<T> Query(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

        Task TruncateTable(string tableName);
    }
}
