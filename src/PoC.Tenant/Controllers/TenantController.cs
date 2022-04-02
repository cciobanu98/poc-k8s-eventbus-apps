using Dapr;
using Microsoft.AspNetCore.Mvc;
using PoC.Shared;

namespace PoC.Tenant.Controllers
{
    [ApiController]
    [Route("tenant")]
    public class TenantController : SharedController
    {
        private readonly IStore _store;
        private readonly IEventBus _eventBus;
        private readonly ILogger<TenantController> _logger;
        private const string DAPR_PUBSUB_NAME = "pubsub";

        public TenantController(IStore store, IEventBus eventBus, ILogger<TenantController> logger) : base(store)
        {
            _store = store;
            _eventBus = eventBus;
            _logger = logger;
        }

        [HttpPost("send-tenant-message")]
        public async Task<ActionResult> SendTenantMessageAsync(string message)
        {
            await _eventBus.PublishAsync(new TenantIntegrationEvent(message), DAPR_PUBSUB_NAME);
            return Ok();
        }

        [HttpPost("OperatorIntegrationEvent")]
        [Topic(DAPR_PUBSUB_NAME, nameof(OperatorIntegrationEvent))]
        public void Handle(OperatorIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

    }
}
