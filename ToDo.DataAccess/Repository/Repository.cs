using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Data;
using ToDo.DataAccess.Repository.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties

        private readonly ApplicationDbContext dbContext;
        internal DbSet<T> dbSet;

        #endregion

        #region CTOR

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        #endregion

        #region Add

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        #endregion

        #region Get

        public T Get(Expression<Func<T, bool>> filters, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet.Where(filters);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        #endregion

        #region GetAll

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        #endregion

        #region Remove

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        #endregion

        #region GetAllBySearch

        public IEnumerable<T> GetAllBySearch(Expression<Func<T, bool>> filters, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet.Where(filters);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        #endregion
    }
}
