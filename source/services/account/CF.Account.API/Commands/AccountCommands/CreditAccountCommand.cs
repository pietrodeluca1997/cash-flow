using CF.CustomMediator.Models;

namespace CF.Account.API.Commands.AccountCommands
{
    public class CreditAccountCommand : Command
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public decimal MoneyAmount { get; set; }
        public string Description { get; set; }

        public CreditAccountCommand(Guid userId, string email, decimal moneyAmount, string description)
        {
            UserId = userId;
            Email = email;
            MoneyAmount = moneyAmount;
            Description = description;
        }

        public override bool IsValid()
        {
            return MoneyAmount > 0;
        }
    }
}
