using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IMonitoringRepository : IRepository<Monitoring>
    {
        void Update(Monitoring obj);

    }
}
