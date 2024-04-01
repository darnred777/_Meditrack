using Meditrack.Data;
using Meditrack.Repository.IRepository;

namespace Meditrack.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        //public IUserRepository User { get; private set; }
        //public IUserGroupRepository UserGroup { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductCategoryRepository ProductCategory { get; private set; }
        public ILocationRepository Location { get; private set; }
        public IPurchaseRequisitionHeaderRepository PurchaseRequisitionHeader { get; private set; }
        public IPurchaseRequisitionDetailRepository PurchaseRequisitionDetail { get; private set; }
        public IPurchaseOrderHeaderRepository PurchaseOrderHeader { get; private set; }
        public IPurchaseOrderDetailRepository PurchaseOrderDetail { get; private set; }
        public IStatusRepository Status { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            //User = new UserRepository(_db);
            //UserGroup = new UserGroupRepository(_db);
            Supplier = new SupplierRepository(_db);
            Product = new ProductRepository(_db);
            ProductCategory = new ProductCategoryRepository(_db);
            Location = new LocationRepository(_db);
            PurchaseRequisitionHeader = new PurchaseRequisitionHeaderRepository(_db);
            PurchaseRequisitionDetail = new PurchaseRequisitionDetailRepository(_db);
            PurchaseOrderHeader = new PurchaseOrderHeaderRepository(_db);
            PurchaseOrderDetail = new PurchaseOrderDetailRepository(_db);
            Status = new StatusRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
