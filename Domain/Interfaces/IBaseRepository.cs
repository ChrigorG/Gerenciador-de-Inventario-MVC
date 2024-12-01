
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntities
    {
        Task<List<T>> Get();
        Task<T?> Get(int id);
        Task<T?> Add(T entity);
        Task<T?> Update(T entity);
        Task<T?> Delete(T entity);
    }
}
