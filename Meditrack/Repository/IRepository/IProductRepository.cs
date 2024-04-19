using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);

        Product GetProductById(int id);

        IEnumerable<Product> GetAllProducts();
        // or
        IQueryable<Product> Products { get; }

        bool HasDependencies(int productId);
    }
}
