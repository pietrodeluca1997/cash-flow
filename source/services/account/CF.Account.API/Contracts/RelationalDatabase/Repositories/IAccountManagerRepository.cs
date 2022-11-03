using CF.Account.API.Entities;

namespace CF.Account.API.Contracts.RelationalDatabase.Repositories
{
    public interface IAccountManagerRepository
    {
        public IQueryable<AccountManager> GetById(int accountManagerId, bool trackChanges);
        public IQueryable<AccountManager> GetByCPFNumber(string cpfNumber, bool trackChanges);
        public void Create(AccountManager accountManager);
    }
}
