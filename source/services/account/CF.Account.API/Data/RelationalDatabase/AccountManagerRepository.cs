using CF.Account.API.Contracts.RelationalDatabase.Repositories;
using CF.Account.API.Entities;

namespace CF.Account.API.Data.RelationalDatabase
{
    public class AccountManagerRepository : RepositoryBase<AccountManager>, IAccountManagerRepository
    {
        public AccountManagerRepository(AccountDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public IQueryable<AccountManager> GetByCPFNumber(string cpfNumber, bool trackChanges)
        {
            return FindByCondition(accountManager => accountManager.CPFNumber.Equals(cpfNumber), trackChanges);
        }

        public IQueryable<AccountManager> GetById(int accountManagerId, bool trackChanges)
        {
            return FindByCondition(accountManager => accountManager.Id.Equals(accountManagerId), trackChanges);
        }
    }
}
