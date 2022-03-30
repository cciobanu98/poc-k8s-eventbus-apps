using Dapr.Client;
using Microsoft.Extensions.Logging;

namespace PoC.Shared
{
    public class DaprEventBus : IEventBus
    {
        private readonly DaprClient _dapr;
        private readonly ILogger _logger;

        public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
        {
            _dapr = dapr;
            _logger = logger;
        }

        public async Task PublishAsync(IntegrationEvent integrationEvent, string pubsubName = "pubsub")
        {
            var topicName = integrationEvent.GetType().Name;

            _logger.LogInformation(
                "Publishing event {@Event} to {PubsubName}.{TopicName}",
                integrationEvent,
                pubsubName,
                topicName);

            // We need to make sure that we pass the concrete type to PublishEventAsync,
            // which can be accomplished by casting the event to dynamic. This ensures
            // that all event fields are properly serialized.
            await _dapr.PublishEventAsync(pubsubName, topicName, (object)integrationEvent);
        }
    }
}
