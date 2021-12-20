using CloudHub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CloudHub.Data.Repositories
{

    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(Microsoft.EntityFrameworkCore.DbContext context) => _context = context;


        private readonly Microsoft.EntityFrameworkCore.DbContext _context;

        private DbSet<T> DbSet
        {
            get
            {
                return _context.Set<T>();
            }
        }

        public async Task<T> Add(T entity)
        {
            EntityEntry<T> result = await DbSet.AddAsync(entity);
            return result.Entity;
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }


        public async Task<List<T>> Where(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = DbSet.Where(expression);
            List<T> results = await query.ToListAsync();
            return results;
        }

        public async Task<List<T>> Where(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = DbSet.Where(expression);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            List<T> results = await query.ToListAsync();
            return results;
        }

        public async Task<T?> FirstWhere(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = DbSet.Where(expression);
            T? results = await query.FirstOrDefaultAsync();
            return results;
        }

        public async Task<T?> FirstWhere(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet.Where(expression);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            T? result = await query.FirstOrDefaultAsync();
            return result;
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public async Task<T?> GetByPk(object id)
        {
            T? result = await DbSet.FindAsync(id);
            return result;
        }

        public async Task<List<T>> GetAll()
        {
            List<T> all = await DbSet.ToListAsync();
            return all;
        }

        public void DeleteMultiple(List<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
