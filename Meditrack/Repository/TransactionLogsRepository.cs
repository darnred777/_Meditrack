using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
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

        public void Update(TransactionLogs obj)
        {
            _db.TransactionLogs.Update(obj);
        }
    }
}
