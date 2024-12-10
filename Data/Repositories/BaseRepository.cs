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

        // Somente é permitido usar o defaultUserId para criar registros ao iniciar o sistema
        public T? Add(T entity, int? defaultUserId = null)
        {
            try
            {
                // Buscando o Id do usuario Logado
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

                int userId = 0;
                // Se não foi possivel capturar o userId logado e não é inicialização do sistema, retorna null
                if ((userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId)) && defaultUserId == null)
                    return null;
                else if ((userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId)) && defaultUserId != null)
                    userId = defaultUserId ?? 0; // Id (0) está sendo tratado como o proprio sistema que está criando o elemento
                
                entity.IdUserCreated = userId;
                entity.DatetimeCreate = DateTime.Now;
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
                // Buscando o Id do usuario Logado
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    entity.IdUserUpdated = userId;
                    entity.DatetimeUpdated = DateTime.Now;
                    _db.Update(entity);
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
