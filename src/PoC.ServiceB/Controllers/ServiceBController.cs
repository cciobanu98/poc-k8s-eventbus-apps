using Dapr;
using Microsoft.AspNetCore.Mvc;
using PoC.Shared;

namespace PoC.ServiceB.Controllers
{
    [ApiController]
    [Route("service-b")]
    public class ServiceBController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME_A = "pubsubA";
        private const string DAPR_PUBSUB_NAME_B = "pubsubB";
        private readonly IEventBus _eventBus;
        private readonly ILogger<ServiceBController> _logger;
        private IList<string> Messages = new List<string>();
        public ServiceBController(IEventBus eventBus, ILogger<ServiceBController> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        [HttpPost("to-a")]
        public async Task<ActionResult> SendMessageBToQeueA()
        {
            await _eventBus.PublishAsync(new MessageB("Message from service B"), DAPR_PUBSUB_NAME_A);
            return Ok();
        }

        [HttpPost("MessageA")]
        [Topic(DAPR_PUBSUB_NAME_B, nameof(MessageA))]
        public async Task HandleAsync(MessageA @event)
        {
            Messages.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

        [HttpGet("messages")]
        public async Task<ActionResult<IEnumerable<string>>> GetMessages() => Ok(Messages);
    }
}
