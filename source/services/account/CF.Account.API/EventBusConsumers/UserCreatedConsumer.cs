using CF.Account.API.Contracts.Services;
using CF.Core.Messages.IntegrationEvents;
using MassTransit;

namespace CF.Account.API.EventBusConsumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IAccountManagerServices _accountManagerServices;

        public UserCreatedConsumer(IAccountManagerServices accountManagerServices)
        {
            _accountManagerServices = accountManagerServices;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            await _accountManagerServices.Create(context.Message);
        }
    }
}
