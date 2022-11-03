using CF.CustomMediator.Models;

namespace CF.CustomMediator.Contracts
{
    public interface INotificationSubscriber<TNotificationMessage> where TNotificationMessage : NotificationMessage
    {
        Task Receive(TNotificationMessage notificationMessage);
    }
}
