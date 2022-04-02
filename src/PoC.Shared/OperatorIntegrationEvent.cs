namespace PoC.Shared
{
    public class OperatorIntegrationEvent : IntegrationEvent
    {
        public OperatorIntegrationEvent(string message) : base()
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
