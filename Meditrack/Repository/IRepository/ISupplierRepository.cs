using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Supplier GetSupplierLocationById(int supplierId);
        void Update(Supplier obj);
    }
}
