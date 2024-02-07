namespace HxcApi.Common.Events.Contracts;

public interface IEventHandler<TEvent>
    where TEvent : IEvent
{
    Task HandleAsync(TEvent eventToHandle);
}