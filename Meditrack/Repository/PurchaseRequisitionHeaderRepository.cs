using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
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

        public PurchaseRequisitionHeader GetFirstOrDefault(Expression<Func<PurchaseRequisitionHeader, bool>> filter, string includeProperties = "")
        {
            IQueryable<PurchaseRequisitionHeader> query = dbSet;

            query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault();
        }

        public bool Any(Expression<Func<PurchaseRequisitionHeader, bool>> filter)
        {
            return _db.PurchaseRequisitionHeader.Any(filter);
        }
    }
}
