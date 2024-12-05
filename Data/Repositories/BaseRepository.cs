using Data.Context;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntities
    {
        protected readonly AppDbContext _db;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool Any() => _db.Set<T>().Any();

        public List<T> Get()
        {
            return _db.Set<T>().Where(x => x.StatusDeleted != true).ToList();
        }

        public T? Get(int id)
        {
            return _db.Set<T>().FirstOrDefault(x => x.Id == id && x.StatusDeleted != true);
        }

        public T? Add(T entity)
        {
            try
            {
                entity.DatetimeCreate = DateTime.UtcNow;
                _db.Add(entity);
                _db.SaveChanges();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public T? Update(T entity)
        {
            try
            {
                entity.DatetimeUpdated = DateTime.UtcNow;
                _db.Update(entity);
                _db.SaveChanges();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public T? Delete(T entity)
        {
            entity.StatusDeleted = true;
            return Update(entity);
        }
    }
}
