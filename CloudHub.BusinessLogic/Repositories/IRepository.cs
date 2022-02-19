using System.Linq.Expressions;

namespace CloudHub.BusinessLogic.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetByPk(object id);
        Task<T> Add(T entity);
        void Delete(T entity);
        void DeleteMultiple(List<T> entities);
        Task<List<T>> Where(Expression<Func<T, bool>> expression);
        Task<T?> FirstWhere(Expression<Func<T, bool>> expression);
        void Update(T entity);
        Task<List<T>> Where(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<T?> FirstWhere(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task SaveBulk(List<T> userActions);
        Task<bool> Any(Expression<Func<T, bool>> expression);
    }
}
