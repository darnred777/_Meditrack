using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class MonitoringRepository : Repository<Monitoring>, IMonitoringRepository
    {
        private ApplicationDbContext _db;
        public MonitoringRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Monitoring obj)
        {
            _db.Monitoring.Update(obj);
        }
    }
}
