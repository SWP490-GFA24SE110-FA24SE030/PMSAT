using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using LAK.Sdk.Core.Utilities;
using PMSAT.DataTier.Paginate;
using PMSAT.DataTier.Repository.Interfaces;

namespace PMSAT.DataTier.Repository.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        #region Get Async
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool asNoTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);

            return asNoTracking ? await query.AsNoTracking().FirstOrDefaultAsync() : await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);

            return asNoTracking ? await query.AsNoTracking().Select(selector).FirstOrDefaultAsync() : await query.Select(selector).FirstOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = true, object filter = null, string sort = null, string order = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);

            if (filter != null) query = query.DynamicFilter(filter);
            if (orderBy != null) query = orderBy(query);
            if (sort != null) query = query.DynamicSort(sort, order);

            return asNoTracking ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
        }

        public virtual async Task<ICollection<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = true, object filter = null,
            string sort = null, string order = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);

            if (filter != null) query = query.DynamicFilter(filter);
            if (orderBy != null) query = orderBy(query);
            if (sort != null) query = query.DynamicSort(sort, order);

            return asNoTracking ? await query.AsNoTracking().Select(selector).ToListAsync() : await query.Select(selector).ToListAsync();
        }

        public Task<IPaginate<T>> GetPagingListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1,
            int size = 10, object filter = null, string sort = null, string order = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);

            if (filter != null) query = query.DynamicFilter(filter);
            if (orderBy != null) query = orderBy(query);
            if (sort != null) query = query.DynamicSort(sort, order);

            return query.AsNoTracking().ToPaginateAsync(page, size, 1);
        }

        public Task<IPaginate<TResult>> GetPagingListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1, int size = 10, object filter = null, string sort = null, string order = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);

            if (filter != null) query = query.DynamicFilter(filter);
            if (orderBy != null) query = orderBy(query);
            if (sort != null) query = query.DynamicSort(sort, order);

            return query.AsNoTracking().Select(selector).ToPaginateAsync(page, size, 1);
        }

        #endregion

        #region Insert

        public async Task InsertAsync(T entity)
        {
            if (entity == null) return;
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region Update
        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        #endregion
    }
}
