namespace PoC.Shared
{
    public class TenantIntegrationEvent : IntegrationEvent
    {
        public TenantIntegrationEvent(string message) : base()
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
