using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor) { }
    }
}
