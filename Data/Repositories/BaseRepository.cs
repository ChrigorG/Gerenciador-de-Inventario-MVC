using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntities
    {
        protected readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpContent? _httpContent;

        public BaseRepository(AppDbContext db, 
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        protected HttpContext? CurrentHttpContext => _httpContextAccessor.HttpContext;

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
                // Buscando o Id do usuario Logado
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    entity.IdUserCreated = userId;
                    entity.DatetimeCreate = DateTime.UtcNow;
                    _db.Add(entity);
                    _db.SaveChanges();
                    return entity;
                }
                return null;
            } catch (Exception)
            {
                return null;
            }
        }

        public T? Update(T entity)
        {
            try
            {
                // Buscando o Id do usuario Logado
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    entity.IdUserUpdated = userId;
                    entity.DatetimeUpdated = DateTime.UtcNow;
                    _db.Add(entity);
                    _db.SaveChanges();
                    return entity;
                }
                return null;
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
