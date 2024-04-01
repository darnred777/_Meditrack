//using Meditrack.Data;
//using Meditrack.Models;
//using Meditrack.Repository.IRepository;
//using System.Linq.Expressions;

//namespace Meditrack.Repository
//{
//    public class UserRepository : Repository<User>, IUserRepository
//    {
//        private ApplicationDbContext _db;
//        public UserRepository(ApplicationDbContext db) : base(db)
//        {
//            _db = db;
//        }

//        public void Update(User obj)
//        {
//            _db.User.Update(obj);
//        }
//    }
//}
