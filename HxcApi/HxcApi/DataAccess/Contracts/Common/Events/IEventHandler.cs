namespace HxcApi.DataAccess.Contracts.Common.Events;

public interface IEventHandler<TEvent>
    where TEvent : IEvent
{
    Task HandleAsync(TEvent eventToHandle);
}