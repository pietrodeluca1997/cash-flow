namespace CF.CustomMediator.Models
{
    public class NotificationMessage
    {
        public string MessageType { get; protected set; }

        public NotificationMessage()
        {
            MessageType = GetType().Name;
        }
    }
}
