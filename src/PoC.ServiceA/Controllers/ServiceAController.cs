using Dapr;
using Microsoft.AspNetCore.Mvc;
using PoC.Shared;

namespace PoC.ServiceA.Controllers
{
    [ApiController]
    [Route("service-a")]
    public class ServiceAController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME = "pubsub";
        private readonly IEventBus _eventBus;
        private readonly ILogger<ServiceAController> _logger;
        private IList<string> Messages = new List<string>();
        public ServiceAController(IEventBus eventBus, ILogger<ServiceAController> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessageA()
        {
            await _eventBus.PublishAsync(new MessageA("Message from service A"));
            return Ok();
        }

        [HttpPost("MessageB")]
        [Topic(DAPR_PUBSUB_NAME, nameof(MessageB))]
        public async Task HandleAsync(MessageB @event)
        {
            Messages.Add(@event.Message);
            _logger.LogInformation(@event.Message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetMessages() => Ok(Messages);
    }
}
