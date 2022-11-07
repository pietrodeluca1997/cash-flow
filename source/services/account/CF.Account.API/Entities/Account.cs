using CF.Core.DomainObjects.Abstracts;

namespace CF.Account.API.Entities
{
    public class Account : Entity
    {
        public decimal MoneyAmount { get; set; }

        //EF Core
        public Account()
        {
            UniqueIdentifier = Guid.NewGuid();
        }

        public Account(decimal moneyAmount)
        {
            if(moneyAmount < 0)
            {
                throw new ArgumentException("To open an account money amount should be zero or bigger");
            }

            MoneyAmount = moneyAmount;
        }

        public bool Credit(decimal creditValue)
        {
            if (creditValue <= 0) return false;

            MoneyAmount += creditValue;
            return true;
        }

        public bool Debit(decimal debitValue)
        {
            if (debitValue <= 0) return false;

            bool hasFunds = (MoneyAmount - debitValue) >= 0;

            if (hasFunds)
            {
                MoneyAmount -= debitValue;
            }

            return hasFunds;
        }
    }
}
