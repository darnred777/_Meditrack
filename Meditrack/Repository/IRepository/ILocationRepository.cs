using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface ILocationRepository : IRepository<Location>
    {
        void Update(Location obj);
    }
}
