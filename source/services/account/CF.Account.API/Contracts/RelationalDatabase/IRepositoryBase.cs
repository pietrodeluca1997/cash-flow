using System.Linq.Expressions;

namespace CF.Account.API.Contracts.RelationalDatabase
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Extract(Expression<Func<T, T>> extraction, IQueryable<T> entities);
    }
}
