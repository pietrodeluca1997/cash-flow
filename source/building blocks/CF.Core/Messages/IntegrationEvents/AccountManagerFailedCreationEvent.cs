using CF.CustomMediator.Models;

namespace CF.Core.Messages.IntegrationEvents
{
    public class AccountManagerFailedCreationEvent : Event
    {
        public Guid UserId { get; set; }

        public AccountManagerFailedCreationEvent() : base()
        {

        }

        public AccountManagerFailedCreationEvent(Guid userId) : base()
        {
            UserId = userId;
        }
    }
}
