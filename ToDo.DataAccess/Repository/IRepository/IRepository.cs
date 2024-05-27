using System.Linq.Expressions;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllEntityFromDb(string? includeProperties = null);
        Task<T> GetFirstEntityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task<IEnumerable<T>> GetAllEnitityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null);
    }
}
