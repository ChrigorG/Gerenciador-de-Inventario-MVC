using Data.Context;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IBaseRepository<Product>
    {
        public ProductRepository(AppDbContext db) : base(db) { }
    }
}
