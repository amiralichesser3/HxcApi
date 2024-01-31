namespace HxcApi.DataAccess.Contracts.Common.Events;

public interface IEventPublisher<T>
{
    Task PublishAsync(T @event);
}