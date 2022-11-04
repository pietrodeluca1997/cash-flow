using System.Linq.Expressions;

namespace CF.Core.Repositories
{
    public interface IQueryBaseRepository<TQueryDocument> where TQueryDocument : IQueryDocument
    {
        Task<IList<TQueryDocument>> ListAllAsync();
        Task<TQueryDocument> GetByIdAsync(string id);
        Task<IList<TQueryDocument>> FilterByAsync(Expression<Func<TQueryDocument, bool>> filterExpression);
        Task CreateAsync(TQueryDocument document);
        Task CreateManyAsync(IList<TQueryDocument> documents);
        Task<bool> UpdateAsync(TQueryDocument document);
        Task<bool> DeleteAsync(string id);
    }
}
