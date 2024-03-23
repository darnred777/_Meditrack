using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IPurchaseRequisitionDetailRepository : IRepository<PurchaseRequisitionDetail>
    {
        void Update(PurchaseRequisitionDetail obj);
    }
}
