namespace CF.Report.API.Settings
{
    public class EventBusSettings
    {
        public string HostAddress { get; set; }

        public string TransactionPerformedEventQueue { get; set; }
        public string InsufficientFundsEventQueue { get; set; }

        public EventBusSettings()
        {

        }

        public EventBusSettings(string hostAddress, string transactionPerformedEventQueue, string insufficientFundsEventQueue)
        {
            HostAddress = hostAddress;
            TransactionPerformedEventQueue = transactionPerformedEventQueue;
            InsufficientFundsEventQueue = insufficientFundsEventQueue;
        }
    }
}
