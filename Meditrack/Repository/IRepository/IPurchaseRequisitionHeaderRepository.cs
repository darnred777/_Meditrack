using Meditrack.Models;
using System.Linq.Expressions;

namespace Meditrack.Repository.IRepository
{
    public interface IPurchaseRequisitionHeaderRepository : IRepository<PurchaseRequisitionHeader>
    {
        void Update(PurchaseRequisitionHeader obj);
        PurchaseRequisitionHeader GetFirstOrDefault(Expression<Func<PurchaseRequisitionHeader, bool>> filter, string includeProperties = "");

        bool Any(Expression<Func<PurchaseRequisitionHeader, bool>> filter);
    }
}
