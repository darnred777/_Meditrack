using Meditrack.Data;
using Meditrack.Repository.IRepository;

namespace Meditrack.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public IUserGroupRepository UserGroup { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductCategoryRepository ProductCategory { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            UserGroup = new UserGroupRepository(_db);
            Supplier = new SupplierRepository(_db);
            Product = new ProductRepository(_db);
            ProductCategory = new ProductCategoryRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
