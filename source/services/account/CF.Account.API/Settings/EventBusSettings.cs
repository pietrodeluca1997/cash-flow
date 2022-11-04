namespace CF.Account.API.Settings
{
    public class EventBusSettings
    {
        public string HostAddress { get; set; }
        public string CreditTransactionRequestedQueue { get; set; }
        public string DebitTransactionRequestedQueue { get; set; }

        public EventBusSettings()
        {

        }

        public EventBusSettings(string hostAddress, string creditTransactionRequestedQueue, string debitTransactionRequestedQueue)
        {
            HostAddress = hostAddress;
            CreditTransactionRequestedQueue = creditTransactionRequestedQueue;
            DebitTransactionRequestedQueue = debitTransactionRequestedQueue;
        }
    }
}
