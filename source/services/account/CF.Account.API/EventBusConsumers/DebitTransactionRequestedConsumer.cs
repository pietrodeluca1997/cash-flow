using CF.Account.API.Contracts.Services;
using CF.Core.Helpers;
using CF.Core.Messages.IntegrationEvents;
using CF.Core.Repositories;
using MassTransit;

namespace CF.Account.API.EventBusConsumers
{
    public class DebitTransactionRequestedConsumer : IConsumer<DebitTransactionRequestedEvent>
    {
        private readonly IMemoryDatabaseRepository _memoryDatabaseRepository;
        private readonly IAccountServices _accountServices;

        public DebitTransactionRequestedConsumer(IMemoryDatabaseRepository memoryDatabaseRepository, IAccountServices accountServices)
        {
            _memoryDatabaseRepository = memoryDatabaseRepository;
            _accountServices = accountServices;
        }

        public async Task Consume(ConsumeContext<DebitTransactionRequestedEvent> context)
        {
            string transactionKey = $"Account_{1}_TransactionOcurring";
            string transactionValue = await _memoryDatabaseRepository.GetValueByKey(transactionKey);

            if (transactionValue is not null)
            {
                await context.SchedulePublish(DateTime.Now.AddSeconds(15), context.Message);
                return;
            }

            await _memoryDatabaseRepository.AddKeyAsync(transactionKey, JsonHelper.ToJson(context.Message));

            await _accountServices.Debit(context.Message);

            await _memoryDatabaseRepository.DeleteByKey(transactionKey);
        }
    }
}
