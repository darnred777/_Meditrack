using Meditrack.Data;
using Meditrack.Models;
using Meditrack.Repository.IRepository;
using System.Linq.Expressions;

namespace Meditrack.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Location obj)
        {
            _db.Location.Update(obj);
        }

        public bool ExistsWithRelation(int locationId)
        {
            // Check if any Supplier, ApplicationUser, or PurchaseRequisitionHeader is linked to this Location
            bool existsSupplier = _db.Supplier.Any(s => s.LocationID == locationId);
            bool existsPurchaseRequisition = _db.PurchaseRequisitionHeader.Any(pr => pr.LocationID == locationId);
            bool existsApplicationUser = _db.ApplicationUser.Any(au => au.LocationID == locationId);

            return existsSupplier || existsPurchaseRequisition || existsApplicationUser;
        }
    }
}
