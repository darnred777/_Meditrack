using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        void Update(ProductCategory obj);

        bool HasProducts(int categoryId);
    }
}
