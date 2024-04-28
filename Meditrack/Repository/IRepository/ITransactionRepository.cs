using Meditrack.Models;

namespace Meditrack.Repository.IRepository
{
    public interface ITransactionLogsRepository : IRepository<TransactionLogs>
    {
        void Update(TransactionLogs obj);
    }
}
