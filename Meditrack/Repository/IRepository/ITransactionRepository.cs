using Meditrack.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Meditrack.Repository.IRepository
{
    public interface ITransactionLogsRepository : IRepository<TransactionLogs>
    {
        Task<IEnumerable<TransactionLogs>> GetAllAsync(string includeProperties);
        Task<List<TransactionLogs>> GetTransactionLogsWithDetailsAsync(
            Expression<Func<TransactionLogs, bool>> filter = null,
            string includeProperties = ""
        );

        void Update(TransactionLogs obj);
    }
}
