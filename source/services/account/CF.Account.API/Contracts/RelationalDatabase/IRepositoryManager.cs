using CF.Account.API.Contracts.RelationalDatabase.Repositories;

namespace CF.Account.API.Contracts.RelationalDatabase
{
    public interface IRepositoryManager
    {
        IAccountRepository AccountRepository { get; }
        IAccountManagerRepository AccountManagerRepository { get; }
        void Save();
    }
}
