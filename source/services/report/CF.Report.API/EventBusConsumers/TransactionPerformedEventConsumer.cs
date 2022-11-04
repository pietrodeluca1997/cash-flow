using AutoMapper;
using CF.Core.Messages.IntegrationEvents;
using CF.Core.Repositories;
using CF.Report.API.Data.QueryDatabase;
using MassTransit;

namespace CF.Report.API.EventBusConsumers
{
    public class TransactionPerformedEventConsumer : IConsumer<TransactionPerformedEvent>
    {
        private readonly IQueryBaseRepository<TransactionReportDocument> _queryBaseRepository;
        private readonly IMapper _mapper;

        public TransactionPerformedEventConsumer(IQueryBaseRepository<TransactionReportDocument> queryBaseRepository, IMapper mapper)
        {
            _queryBaseRepository = queryBaseRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<TransactionPerformedEvent> context)
        {
            TransactionReportDocument document = _mapper.Map<TransactionPerformedEvent, TransactionReportDocument>(context.Message);
            await _queryBaseRepository.CreateAsync(document);
        }
    }
}
