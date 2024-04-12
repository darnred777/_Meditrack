using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Product.Update(obj);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Product.ToList();
        }

        // or

        public IQueryable<Product> Products => _db.Product;
        public Product GetProductById(int id)
        {
            return _db.Product.FirstOrDefault(p => p.ProductID == id);
        }
    }
}
