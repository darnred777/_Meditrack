using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IPurchaseRequisitionHeaderRepository : IRepository<PurchaseRequisitionHeader>
    {
        void Update(PurchaseRequisitionHeader obj);
    }
}
