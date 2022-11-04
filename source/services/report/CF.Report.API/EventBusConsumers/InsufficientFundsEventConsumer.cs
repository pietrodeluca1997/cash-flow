using CF.Core.Messages.IntegrationEvents;
using MassTransit;

namespace CF.Report.API.EventBusConsumers
{
    public class InsufficientFundsEventConsumer : IConsumer<AccountInsufficientFundsEvent>
    {
        public async Task Consume(ConsumeContext<AccountInsufficientFundsEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
