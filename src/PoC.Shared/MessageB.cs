namespace PoC.Shared
{
    public class MessageB : IntegrationEvent
    {
        public MessageB(string message) : base()
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
