using System.Linq.Expressions;

namespace ToDo.RepositoryLayer.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllEntityFromDb(Expression<Func<T, bool>> filters, string? includeProperties = null);
        Task<T> GetFirstEntityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllEnitityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null);
    }
}
