using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IPurchaseOrderHeaderRepository : IRepository<PurchaseOrderHeader>
    {
        void Update(PurchaseOrderHeader obj);
    }
}
