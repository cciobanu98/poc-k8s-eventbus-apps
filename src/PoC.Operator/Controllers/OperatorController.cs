using Dapr;
using Microsoft.AspNetCore.Mvc;
using PoC.Shared;

namespace PoC.Operator.Controllers
{
    [ApiController]
    [Route("operator")]
    public class OperatorController : SharedController
    {
        private readonly IStore _store;
        private readonly IEventBus _eventBus;
        private readonly ILogger<OperatorController> _logger;
        private const string DAPR_PUBSUB_NAME = "pubsub";

        public OperatorController(IStore store, IEventBus eventBus, ILogger<OperatorController> logger) : base(store)
        {
            _store = store;
            _eventBus = eventBus;
            _logger = logger;
        }

        [HttpPost("send-operator-message")]
        public async Task<ActionResult> SendOperatorMessageAsync(string message)
        {
            await _eventBus.PublishAsync(new OperatorIntegrationEvent(message), DAPR_PUBSUB_NAME);
            return Ok();
        }

        [HttpPost("TenantIntegrationEvent")]
        [Topic(DAPR_PUBSUB_NAME, nameof(TenantIntegrationEvent))]
        public void Handle(TenantIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

    }
}
