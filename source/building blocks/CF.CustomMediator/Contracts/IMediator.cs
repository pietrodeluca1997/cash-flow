using CF.CustomMediator.Models;
using CF.CustomMediator.Models.Enums;

namespace CF.CustomMediator.Contracts
{
    public interface IMediator
    {
        Task SendMessage<TRequestMessage>(TRequestMessage requestMessage) where TRequestMessage : RequestMessage;
        Task Notify<TNotificationMessage>(TNotificationMessage notificationMessage) where TNotificationMessage : NotificationMessage;
        void FinishRequestBehaviour<TRequestMessage>(TRequestMessage requestMessage, ECommandResponseType commandResponseType) where TRequestMessage : Command;
        void FinishRequestBehaviour<TRequestMessage, TCustomCommandResponse>(TRequestMessage requestMessage, ECommandResponseType commandResponseType, TCustomCommandResponse customCommandResponse) where TRequestMessage : Command
                                                                                                                                                                                                     where TCustomCommandResponse : class;
    }
}
