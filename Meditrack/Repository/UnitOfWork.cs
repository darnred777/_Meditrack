using Meditrack.Data;
using Meditrack.Repository.IRepository;

namespace Meditrack.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
