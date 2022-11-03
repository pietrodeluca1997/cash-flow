using CF.Account.API.Contracts.RelationalDatabase;
using CF.Account.API.Contracts.RelationalDatabase.Repositories;

namespace CF.Account.API.Data.RelationalDatabase
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AccountDbContext _repositoryContext;
        private IAccountRepository _accountRepository;
        private IAccountManagerRepository _accountManagerRepository;

        public RepositoryManager(AccountDbContext repositoryContext) => _repositoryContext = repositoryContext;

        public void Save() => _repositoryContext.SaveChanges();

        public IAccountRepository AccountRepository
        {
            get => _accountRepository ??= new AccountRepository(_repositoryContext);
        }
        public IAccountManagerRepository AccountManagerRepository
        {
            get => _accountManagerRepository ??= new AccountManagerRepository(_repositoryContext);
        }
    }
}
