using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);
    }
}
