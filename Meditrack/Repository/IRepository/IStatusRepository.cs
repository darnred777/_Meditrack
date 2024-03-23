using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IStatusRepository : IRepository<Status>
    {
        void Update(Status obj);
    }
}
