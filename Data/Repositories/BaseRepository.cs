using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntities
    {
        protected readonly AppDbContext _db;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<T>> Get()
        {
            return await _db.Set<T>().Where(x => x.StatusDeleted != true).ToListAsync();
        }

        public async Task<T?> Get(int id)
        {
            return await _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.StatusDeleted != true);
        }

        public async Task<T?> Add(T entity)
        {
            try
            {
                entity.DatetimeCreate = DateTime.UtcNow;
                _db.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<T?> Update(T entity)
        {
            try
            {
                entity.DatetimeUpdated = DateTime.UtcNow;
                _db.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<T?> Delete(T entity)
        {
            entity.StatusDeleted = true;
            return await Update(entity);
        }
    }
}
