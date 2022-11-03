using CF.Account.API.Contracts.RelationalDatabase.Repositories;

namespace CF.Account.API.Data.RelationalDatabase
{
    public class AccountRepository : RepositoryBase<Entities.Account>, IAccountRepository
    {
        public AccountRepository(AccountDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public IQueryable<Entities.Account> GetById(int accountId, bool trackChanges)
        {
            return FindByCondition(account => account.Id.Equals(accountId), trackChanges);
        }
    }
}
