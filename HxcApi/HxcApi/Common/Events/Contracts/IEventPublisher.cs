namespace HxcApi.Common.Events.Contracts;

public interface IEventPublisher<T> where T : IEvent
{
    Task PublishAsync(T @event);
}