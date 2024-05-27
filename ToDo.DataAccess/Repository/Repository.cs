﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        #region AddAsync

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        #endregion

        #region GetFirstEntityFromDbBySearchAsync

        public async Task<T> GetFirstEntityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet.Where(filters);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        #endregion

        #region GetAllEntityFromDb

        public IEnumerable<T> GetAllEntityFromDb(string? includeProperties = null)
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

        #region GetAllEnitityFromDbBySearchAsync

        public async Task<IEnumerable<T>> GetAllEnitityFromDbBySearchAsync(Expression<Func<T, bool>> filters, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet.Where(filters);
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
