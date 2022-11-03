using CF.Account.API.Contracts.RelationalDatabase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CF.Account.API.Data.RelationalDatabase
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AccountDbContext AccountDbContext;
        public RepositoryBase(AccountDbContext accountDbContext)
        {
            AccountDbContext = accountDbContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ? AccountDbContext.Set<T>() :
                                  AccountDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ? AccountDbContext.Set<T>().Where(expression) :
                                  AccountDbContext.Set<T>().Where(expression).AsNoTracking();

        }
        public void Create(T entity) => AccountDbContext.Set<T>().Add(entity);
        public void Update(T entity) => AccountDbContext.Set<T>().Update(entity);
        public void Delete(T entity) => AccountDbContext.Set<T>().Remove(entity);
        public IQueryable<T> Extract(Expression<Func<T, T>> extraction, IQueryable<T> entities) => entities.Select(extraction);
        public T? Single(IQueryable<T> entities) => entities.FirstOrDefault();
    }
}
