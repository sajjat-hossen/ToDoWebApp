﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDo.DataAccess.Data;
using ToDo.RepositoryLayer.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        #endregion

        #region Constructor

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #endregion

        #region AddAsync

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        #endregion

        #region GetFirstEntityFromDbBySearchAsync

        public async Task<T> GetFirstEntityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null)
        {
            var query = _dbSet.Where(filters);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region GetAllEntityFromDb

        public IEnumerable<T> GetAllEntityFromDb(Expression<Func<T, bool>> filters, string? includeProperties = null)
        {
            var query = _dbSet.Where(filters);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        #endregion

        #region Remove

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        #endregion

        #region RemoveRange

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        #endregion

        #region GetAllEnitityFromDbBySearchAsync

        public async Task<IEnumerable<T>> GetAllEnitityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null)
        {
            var query = _dbSet.Where(filters);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        #endregion
    }
}
