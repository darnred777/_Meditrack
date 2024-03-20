namespace Meditrack.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IUserGroupRepository UserGroup { get; }
        ISupplierRepository Supplier { get; }
        IProductRepository Product { get; }
        IProductCategoryRepository ProductCategory { get; }
        ILocationRepository Location { get; }

        void Save();
    }
}
