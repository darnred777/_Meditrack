using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private ApplicationDbContext _db;
        public ProductCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductCategory obj)
        {
            _db.ProductCategory.Update(obj);
        }

        public bool HasProducts(int categoryId)
        {
            // Check if any product is linked to this category
            return _db.Product.Any(p => p.CategoryID == categoryId);
        }
    }
}
