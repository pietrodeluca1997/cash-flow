using CF.Core.DomainObjects.Enums;
using CF.CustomMediator.Models;

namespace CF.Core.Messages.IntegrationEvents
{
    public class TransactionPerformedEvent : Event
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
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
            TransactionDate = DateTime.Now;
            TransactionType = eTransactionType.ToString();
            TransactionTypeId = (int)eTransactionType;
        }

        public TransactionPerformedEvent(Guid userId,
                                         string userName,
                                         DateTime transactionDate,
                                         string transactionDescription,
                                         decimal transactionAmount,
                                         ETransactionType eTransactionType)
        {
            UserId = userId;
            UserName = userName;
            TransactionDate = DateTime.Now;
            TransactionDescription = transactionDescription;
            TransactionAmount = transactionAmount;
            TransactionType = eTransactionType.ToString();
            TransactionTypeId = (int)eTransactionType;
        }
    }
}
