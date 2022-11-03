using CF.Core.DomainObjects.Abstracts;

namespace CF.Account.API.Entities
{
    public class Account : Entity
    {
        public decimal MoneyAmount { get; set; }
        public Account()
        {
            UniqueIdentifier = Guid.NewGuid();
        }
    }
}
