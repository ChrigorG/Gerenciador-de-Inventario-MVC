
namespace Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T?> Get(int id);
        Task<T?> Add(T entity);
        Task<T?> Update(T entity);
        Task<T?> Delete(T entity);
    }
}
