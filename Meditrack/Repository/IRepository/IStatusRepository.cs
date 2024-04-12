using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IStatusRepository : IRepository<Status>
    {
        void Update(Status obj);

        Status GetFirstOrDefault(System.Linq.Expressions.Expression<Func<Status, bool>> filter, string includeProperties = null);
    }
}
