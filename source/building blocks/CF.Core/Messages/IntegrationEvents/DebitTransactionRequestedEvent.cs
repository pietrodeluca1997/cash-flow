using CF.CustomMediator.Models;

namespace CF.Core.Messages.IntegrationEvents
{
    public class DebitTransactionRequestedEvent : Event
    {
        public Guid UserId { get; set; }
        public decimal MoneyAmount { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        public DebitTransactionRequestedEvent() : base()
        {

        }

        public DebitTransactionRequestedEvent(Guid userId, decimal moneyAmount, string description, string email)
        {
            UserId = userId;
            MoneyAmount = moneyAmount;
            Description = description;
            Email = email;
        }
    }
}
