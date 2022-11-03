using CF.CustomMediator.Models;
using CF.CustomMediator.Models.Enums;

namespace CF.CustomMediator.Contracts
{
    public interface IRequestBehaviourContext
    {
        Command RequestMessage { get; }
        ECommandResponseType CommandResponseType { get; }
        object? CustomCommandResponse { get; }
        void Finish<TRequestMessage>(TRequestMessage requestMessage, ECommandResponseType commandResponseType) where TRequestMessage : Command;
        void Finish<TRequestMessage, TCustomCommandResponse>(TRequestMessage requestMessage, ECommandResponseType commandResponseType, TCustomCommandResponse? customCommandResponse = null) where TRequestMessage : Command where TCustomCommandResponse : class;
    }
}
