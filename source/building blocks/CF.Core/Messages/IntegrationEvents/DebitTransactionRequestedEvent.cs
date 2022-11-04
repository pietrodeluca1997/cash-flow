using CF.CustomMediator.Models;

namespace CF.Core.Messages.IntegrationEvents
{
    public class DebitTransactionRequestedEvent : Event
    {
        public Guid UserId { get; set; }
        public decimal MoneyAmount { get; set; }
        public string Description { get; set; }

        public DebitTransactionRequestedEvent() : base()
        {

        }

        public DebitTransactionRequestedEvent(Guid userId, decimal moneyAmount, string description)
        {
            UserId = userId;
            MoneyAmount = moneyAmount;
            Description = description;
        }
    }
}
