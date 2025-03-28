using System.Linq.Expressions;

namespace FleetManagement.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<Guid> Create(T model);
        Task Update(T model);
        Task Delete(T model);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllActive();
        Task<T> GetOne(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
    }
}
