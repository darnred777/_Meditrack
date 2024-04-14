using Meditrack.Models;
using System.Linq.Expressions;

namespace Meditrack.Repository.IRepository
{
    public interface IPurchaseOrderHeaderRepository : IRepository<PurchaseOrderHeader>
    {
        void Update(PurchaseOrderHeader obj);

        PurchaseOrderHeader GetFirstOrDefault(Expression<Func<PurchaseOrderHeader, bool>> filter, string includeProperties = null);
    }
}
