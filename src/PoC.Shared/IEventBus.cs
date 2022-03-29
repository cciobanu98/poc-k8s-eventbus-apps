namespace PoC.Shared
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent integrationEvent);
    }
}
