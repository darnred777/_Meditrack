using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class TransactionLogsRepository : Repository<TransactionLogs>, ITransactionLogsRepository
    {
        private ApplicationDbContext _db;
        public TransactionLogsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<IEnumerable<TransactionLogs>> GetAllAsync(string includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransactionLogs>> GetTransactionLogsWithDetailsAsync(
            Expression<Func<TransactionLogs, bool>> filter = null,
            string includeProperties = ""
        )
        {
            IQueryable<TransactionLogs> query = _db.Set<TransactionLogs>();

            // Apply filter if specified
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include related entities based on comma-separated properties
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public IEnumerable<TransactionLogs> GetAll()
        {
            return _db.TransactionLogs.ToList();
        }

        public void Update(TransactionLogs obj)
        {
            _db.TransactionLogs.Update(obj);
        }
    }
}
