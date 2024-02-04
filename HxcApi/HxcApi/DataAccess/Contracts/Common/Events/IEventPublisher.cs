namespace HxcApi.DataAccess.Contracts.Common.Events;

public interface IEventPublisher<T> where T : IEvent
{
    Task PublishAsync(T @event);
}