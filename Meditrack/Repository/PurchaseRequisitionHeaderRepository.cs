using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class PurchaseRequisitionHeaderRepository : Repository<PurchaseRequisitionHeader>, IPurchaseRequisitionHeaderRepository
    {
        private ApplicationDbContext _db;
        public PurchaseRequisitionHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchaseRequisitionHeader obj)
        {
            _db.PurchaseRequisitionHeader.Update(obj);
        }
    }
}
