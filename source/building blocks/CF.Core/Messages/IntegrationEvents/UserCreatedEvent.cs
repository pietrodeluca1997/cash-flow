using CF.CustomMediator.Models;

namespace CF.Core.Messages.IntegrationEvents
{
    public class UserCreatedEvent : Event
    {
        public string UserId { get; set; }
        public string CPFNumber { get; set; }
        public string Name { get; set; }

        public UserCreatedEvent() : base()
        {

        }

        public UserCreatedEvent(string userId, string cPFNumber, string name)
        {
            UserId = userId;
            CPFNumber = cPFNumber;
            Name = name;
        }
    }
}
