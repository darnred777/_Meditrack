using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class PurchaseRequisitionDetailRepository : Repository<PurchaseRequisitionDetail>, IPurchaseRequisitionDetailRepository
    {
        private ApplicationDbContext _db;
        public PurchaseRequisitionDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchaseRequisitionDetail obj)
        {
            _db.PurchaseRequisitionDetail.Update(obj);
        }
    }
}
