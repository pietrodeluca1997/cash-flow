namespace CF.Account.API.Contracts.RelationalDatabase.Repositories
{
    public interface IAccountRepository
    {
        IQueryable<Entities.Account> GetById(int accountId, bool trackChanges);
        void Update(Entities.Account account);
    }
}
