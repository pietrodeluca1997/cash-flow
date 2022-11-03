using CF.CustomMediator.Contracts;
using CF.CustomMediator.Models;
using CF.CustomMediator.Models.Enums;

namespace CF.CustomMediator.Core
{
    public class RequestBehaviourContext : IRequestBehaviourContext
    {
        public Command RequestMessage { get; private set; }

        public ECommandResponseType CommandResponseType { get; private set; }

        public object? CustomCommandResponse { get; private set; }

        public void Finish<TRequestMessage>(TRequestMessage requestMessage, ECommandResponseType commandResponseType) where TRequestMessage : Command
        {
            RequestMessage = requestMessage;
            CommandResponseType = commandResponseType;
        }

        public void Finish<TRequestMessage, TCustomCommandResponse>(TRequestMessage requestMessage, ECommandResponseType commandResponseType, TCustomCommandResponse? customCommandResponse = null) where TRequestMessage : Command
                                                                                                                                                                                                    where TCustomCommandResponse : class
        {
            RequestMessage = requestMessage;
            CommandResponseType = commandResponseType;
            CustomCommandResponse = customCommandResponse;
        }
    }
}
