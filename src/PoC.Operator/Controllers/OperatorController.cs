using Dapr;
using Microsoft.AspNetCore.Mvc;
using PoC.Shared;

namespace PoC.Operator.Controllers
{
    [ApiController]
    [Route("operator")]
    public class OperatorController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME_A = "pubsuba";
        private const string DAPR_PUBSUB_NAME_B = "pubsubb";
        private const string DAPR_PUBSUB_NAME_C = "pubsubc";
        private const string DAPR_PUBSUB_NAME_D = "pubsubd";
        private readonly IEventBus _eventBus;
        private readonly ILogger<OperatorController> _logger;
        private readonly IStore _store;
        public OperatorController(IEventBus eventBus, ILogger<OperatorController> logger, IStore store)
        {
            _eventBus = eventBus;
            _logger = logger;
            _store = store;
        }

        [HttpPost("send-operator-message-to-queue-a")]
        public async Task<ActionResult> SendOperatorMessageToQueueA(string message)
        {
            await _eventBus.PublishAsync(new OperatorIntegrationEvent(message), DAPR_PUBSUB_NAME_A);
            return Ok();
        }

        [HttpPost("send-operator-message-to-queue-c")]
        public async Task<ActionResult> SendOperatorMessageToQueueC(string message)
        {
            await _eventBus.PublishAsync(new OperatorIntegrationEvent(message), DAPR_PUBSUB_NAME_C);
            return Ok();
        }

        [HttpPost("send-operator-message-to-queue-d")]
        public async Task<ActionResult> SendOperatorMessageToQueueD(string message)
        {
            await _eventBus.PublishAsync(new OperatorIntegrationEvent(message), DAPR_PUBSUB_NAME_D);
            return Ok();
        }

        [HttpGet("get-messages")]
        public ActionResult<IEnumerable<string>> GetMessages() => Ok(_store.GetAll());

        [HttpPost("clear")]
        public void Clear() => _store.Clear();


        [HttpPost("GetTenantIntegrationEventFromB")]
        [Topic(DAPR_PUBSUB_NAME_B, nameof(TenantIntegrationEvent))]
        public async Task HandleAsyncB(TenantIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

        [HttpPost("GetTenantIntegrationEventFromC")]
        [Topic(DAPR_PUBSUB_NAME_C, nameof(TenantIntegrationEvent))]
        public async Task HandleAsyncC(TenantIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

        [HttpPost("GetTenantIntegrationEventFromD")]
        [Topic(DAPR_PUBSUB_NAME_D, nameof(TenantIntegrationEvent))]
        public async Task HandleAsyncD(TenantIntegrationEvent @event)
        {
            _store.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }
    }
}
