using CF.Core.DomainObjects.Enums;
using CF.Core.Helpers;
using CF.CustomMediator.Models;

namespace CF.Core.Messages.IntegrationEvents
{
    public class TransactionPerformedEvent : Event
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public decimal TransactionAmount { get; set; }

        public TransactionPerformedEvent()
        {

        }

        public TransactionPerformedEvent(ETransactionType eTransactionType)
        {
            TransactionDate = DateHelper.GetBrazilianTime();
            TransactionType = eTransactionType.ToString();
            TransactionTypeId = (int)eTransactionType;
        }

        public TransactionPerformedEvent(Guid userId,
                                         string email,
                                         string transactionDescription,
                                         decimal transactionAmount,
                                         ETransactionType eTransactionType)
        {
            UserId = userId;
            Email = email;
            TransactionDate = DateHelper.GetBrazilianTime();
            TransactionDescription = transactionDescription;
            TransactionAmount = transactionAmount;
            TransactionType = eTransactionType.ToString();
            TransactionTypeId = (int)eTransactionType;
        }
    }
}
