using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class PurchaseOrderHeaderRepository : Repository<PurchaseOrderHeader>, IPurchaseOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public PurchaseOrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PurchaseOrderHeader obj)
        {
            _db.PurchaseOrderHeader.Update(obj);
        }
    }
}
