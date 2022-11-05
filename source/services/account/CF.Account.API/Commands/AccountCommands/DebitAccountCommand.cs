using CF.CustomMediator.Models;

namespace CF.Account.API.Commands.AccountCommands
{
    public class DebitAccountCommand : Command
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public decimal MoneyAmount { get; set; }
        public string Description { get; set; }

        public DebitAccountCommand(Guid userId, decimal moneyAmount, string description)
        {
            UserId = userId;
            MoneyAmount = moneyAmount;
            Description = description;
        }

        public override bool IsValid()
        {
            return MoneyAmount > 0;
        }
    }
}
