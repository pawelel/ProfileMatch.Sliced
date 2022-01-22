﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using ProfileMatch.Data;

namespace ProfileMatch.Repositories
{
    /// <summary>
    /// Generic Data Manager Class
    /// Uses an Entity Framework Data Context to do CRUD operations
    /// on ANY entity.
    /// Customize to your own liking.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDataContext"></typeparam>
    public class DataManager<TEntity, TDataContext> : IRepository<TEntity>
        where TEntity : class
        where TDataContext : DbContext
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;


        internal DbSet<TEntity> _dbSet;

        public DataManager(IDbContextFactory<ApplicationDbContext> contextFactory)
        {

            this._contextFactory = contextFactory;
        }

        public virtual async Task<bool> Delete(TEntity entityToDelete)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            return await context.SaveChangesAsync() >= 1;
        }

        public virtual async Task<bool> Delete(object id)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            return await Delete(entityToDelete);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            await Task.Delay(1);
            return _dbSet.ToList();
        }
        public virtual async Task<TEntity> GetById(params object[] ids)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();

            return await _dbSet.FindAsync(ids);
        }
        public virtual async Task<bool> ExistById(params object[] ids)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            TEntity data = await _dbSet.FindAsync(ids);
            return data != null;
        }
        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            await _dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entityToUpdate)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            _dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entityToUpdate;
        }

        /// <summary>
        /// Generic Get lets you specify a LINQ filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            try
            {
                // Get the dbSet from the Entity passed in
                IQueryable<TEntity> query = _dbSet;

                // Apply the filter
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Include the specified properties
                if (include != null)
                {
                    query = include(query);
                }

                // Sort
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return null;
            }
        }
        public virtual async Task<TEntity> GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            using ApplicationDbContext context = _contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
            try
            {
                // Get the dbSet from the Entity passed in
                IQueryable<TEntity> query = _dbSet;

                // Apply the filter
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Include the specified properties
                if (include != null)
                {
                    query = include(query);
                }
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return null;
            }
        }
    }
}