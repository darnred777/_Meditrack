using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
    {
        private ApplicationDbContext _db;
        public UserGroupRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserGroup obj)
        {
            _db.UserGroup.Update(obj);
        }
    }
}
