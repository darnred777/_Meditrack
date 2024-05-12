using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private ApplicationDbContext _db;
        public SupplierRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Supplier obj)
        {
            _db.Supplier.Update(obj);
        }

        public Supplier GetSupplierLocationById(int supplierId)
        {
            return _db.Supplier.FirstOrDefault(s => s.SupplierID == supplierId);
        }

        public IQueryable<Supplier> GetAll()
        {
            return _db.Supplier;
        }
    }
}
