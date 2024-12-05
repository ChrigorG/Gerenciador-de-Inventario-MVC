
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntities
    {
        bool Any();
        List<T> Get();
        T? Get(int id);
        T? Add(T entity);
        T? Update(T entity);
        T? Delete(T entity);
    }
}
