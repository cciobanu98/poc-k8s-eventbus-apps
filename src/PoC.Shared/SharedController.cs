using Microsoft.AspNetCore.Mvc;

namespace PoC.Shared
{
    [ApiController]
    public abstract class SharedController : ControllerBase
    {
        private readonly IStore _store;

        public SharedController(IStore store)
        {
            _store = store;
        }

        [HttpGet("get-messages")]
        public ActionResult<IEnumerable<string>> GetMessages() => Ok(_store.GetAll());

        [HttpPost("clear")]
        public void Clear() => _store.Clear();
    }
}
