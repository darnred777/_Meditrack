using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
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

        public PurchaseOrderHeader GetFirstOrDefault(Expression<Func<PurchaseOrderHeader, bool>> filter, string includeProperties = "")
        {
            IQueryable<PurchaseOrderHeader> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include any properties that are required
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }
    }
}
