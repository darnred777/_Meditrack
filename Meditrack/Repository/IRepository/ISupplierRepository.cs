using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Supplier GetSupplierLocationById(int supplierId);

        IQueryable<Supplier> GetAll();

        void Update(Supplier obj);
    }
}
