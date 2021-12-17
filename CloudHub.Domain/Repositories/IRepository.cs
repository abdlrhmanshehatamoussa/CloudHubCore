using System.Linq.Expressions;

namespace CloudHub.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByPk(object id);
        Task<T> Add(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression);
        Task<T?> FirstWhere(Expression<Func<T, bool>> expression);
        void Update(T entity);
    }
}
