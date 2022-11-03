using CF.Core.Messages.IntegrationEvents;
using CF.Identity.API.Contracts.Services;
using MassTransit;

namespace CF.Identity.API.EventBusConsumers
{
    public class AccountManagerFailedCreationConsumer : IConsumer<AccountManagerFailedCreationEvent>
    {
        private readonly IUserServices _userServices;

        public AccountManagerFailedCreationConsumer(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task Consume(ConsumeContext<AccountManagerFailedCreationEvent> context)
        {
            await _userServices.Delete(context.Message);
        }
    }
}
