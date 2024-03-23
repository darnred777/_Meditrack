using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IPurchaseOrderDetailRepository : IRepository<PurchaseOrderDetail>
    {
        void Update(PurchaseOrderDetail obj);
    }
}
