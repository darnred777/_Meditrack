using System.Linq.Expressions;

namespace Meditrack.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = "");
        // Unified GetAll method
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        T Get(Expression<Func<T, bool>> filter, string includeProperties = "");
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }

}
