namespace PoC.Shared
{
    public class MessageA : IntegrationEvent
    {
        public MessageA(string message) : base()
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
