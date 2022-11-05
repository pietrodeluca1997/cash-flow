using CF.Core.DomainObjects.Abstracts;
using CF.Core.Helpers;

namespace CF.Transactions.API.Entities
{
    public class Transaction : Entity
    {
        public decimal MoneyAmount { get; set; }
        public string Description { get; set; }

        public Transaction(decimal moneyAmount, string description)
        {
            MoneyAmount = decimal.Zero;
            Description = string.Empty;
        }

        public static bool IsValid(decimal? moneyAmount, out IList<string> validationErrors)
        {
            validationErrors = new List<string>();

            if (moneyAmount.IsNullOrNeutral())
            {
                validationErrors.Add("Money amount should be informed and cannot be null or equals to zero");
                return false;
            }

            if (moneyAmount < 0)
            {
                validationErrors.Add("Money should be a positive value");
            }

            return !validationErrors.Any();
        }
    }
}
