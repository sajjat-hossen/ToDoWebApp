using System.Linq.Expressions;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filters, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAllBySearch(Expression<Func<T, bool>> filters, string? includeProperties = null);
    }
}
