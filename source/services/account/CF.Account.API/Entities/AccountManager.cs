using CF.Core.DomainObjects.Abstracts;

namespace CF.Account.API.Entities
{
    public class AccountManager : Entity
    {
        public string Name { get; set; }
        public string CPFNumber { get; set; }
        public Guid AccountIdentifier { get; set; }

        public AccountManager()
        {

        }

        public AccountManager(string name, string cpfNumber, Guid userId, Guid accountIdentifier)
        {
            Name = name;
            CPFNumber = cpfNumber;
            AccountIdentifier = accountIdentifier;
            UniqueIdentifier = userId;
        }
    }
}
