using Dapr;
using Microsoft.AspNetCore.Mvc;
using PoC.Shared;

namespace PoC.Tenant.Controllers
{
    [ApiController]
    [Route("tenant")]
    public class TenantController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME_A = "pubsuba";
        private const string DAPR_PUBSUB_NAME_B = "pubsubb";
        private const string DAPR_PUBSUB_NAME_C = "pubsubc";
        private const string DAPR_PUBSUB_NAME_D = "pubsubd";
        private readonly IEventBus _eventBus;
        private readonly ILogger<TenantController> _logger;
        private readonly IStore _store;
        public TenantController(IEventBus eventBus, ILogger<TenantController> logger, IStore store)
        {
            _eventBus = eventBus;
            _logger = logger;
            _store = store;
        }

        [HttpPost("send-tenant-message-to-queue-b")]
        public async Task<ActionResult> SendTenantMessageToQueueB(string message)
        {
            await _eventBus.PublishAsync(new TenantIntegrationEvent(message), DAPR_PUBSUB_NAME_B);
            return Ok();
        }

        [HttpPost("send-tenant-message-to-queue-c")]
        public async Task<ActionResult> SendTenantMessageToQueueC(string message)
        {
            await _eventBus.PublishAsync(new TenantIntegrationEvent(message), DAPR_PUBSUB_NAME_C);
            return Ok();
        }

        [HttpPost("send-tenant-message-to-queue-d")]
        public async Task<ActionResult> SendTenantMessageToQueueD(string message)
        {
            await _eventBus.PublishAsync(new TenantIntegrationEvent(message), DAPR_PUBSUB_NAME_D);
            return Ok();
        }

        [HttpGet("get-messages")]
        public ActionResult<IEnumerable<string>> GetMessages() => Ok(_store.GetAll());

        [HttpPost("clear")]
        public void Clear() => _store.Clear();

        [HttpPost("GetOperatorIntegrationEventFromA")]
        [Topic(DAPR_PUBSUB_NAME_A, nameof(OperatorIntegrationEvent))]
        public async Task HandleAsyncA(OperatorIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

        [HttpPost("GetOperatorIntegrationEventFromC")]
        [Topic(DAPR_PUBSUB_NAME_C, nameof(OperatorIntegrationEvent))]
        public async Task HandleAsyncC(OperatorIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

        [HttpPost("GetOperatorIntegrationEventFromD")]
        [Topic(DAPR_PUBSUB_NAME_D, nameof(OperatorIntegrationEvent))]
        public async Task HandleAsyncD(OperatorIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }


    }
}
