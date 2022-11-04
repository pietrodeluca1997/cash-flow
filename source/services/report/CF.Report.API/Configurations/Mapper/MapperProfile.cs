using AutoMapper;
using CF.Core.Messages.IntegrationEvents;
using CF.Report.API.Data.QueryDatabase;

namespace CF.Report.API.Configurations.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TransactionPerformedEvent, TransactionReportDocument>();
        }
    }
}
