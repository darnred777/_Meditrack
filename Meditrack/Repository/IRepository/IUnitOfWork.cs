using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //IUserRepository User { get; }
        //IUserGroupRepository UserGroup { get; }
        ISupplierRepository Supplier { get; }
        IProductRepository Product { get; }
        IProductCategoryRepository ProductCategory { get; }
        ILocationRepository Location { get; }
        IPurchaseRequisitionHeaderRepository PurchaseRequisitionHeader { get; }
        IPurchaseRequisitionDetailRepository PurchaseRequisitionDetail { get; }
        IPurchaseOrderHeaderRepository PurchaseOrderHeader { get; }
        IPurchaseOrderDetailRepository PurchaseOrderDetail { get; }
        IStatusRepository Status { get; }
        ITransactionLogsRepository TransactionLogs { get; }
        IMonitoringRepository Monitoring { get; }

        void Save();
    }
}
