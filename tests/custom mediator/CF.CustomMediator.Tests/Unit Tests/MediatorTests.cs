using CF.CustomMediator.Configuration;
using CF.CustomMediator.Contracts;
using CF.Tests.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CF.CustomMediator.Tests.Unit_Tests
{
    public class MediatorTests
    {
        [Fact(DisplayName = "Mediator Handling Time Benchmark")]
        [Trait("Category", "Mediator Benchmark")]
        public async Task Mediator_OnSendMessage_ShouldReturnAfterProcessmentInLessThanThreeMilliseconds()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMediator(typeof(MediatorTests));
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IMediator mediator = serviceProvider.GetRequiredService<IMediator>();

            TimeSpan timeElapsed = BenchmarkHelper.MeasureTime(async () =>
            {

            });

            Assert.True(timeElapsed.TotalMilliseconds <= 3, $"Elapsed time in milliseconds: {timeElapsed.TotalMilliseconds}");
        }
    }
}
