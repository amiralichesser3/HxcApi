using HxcApi.DataAccess.Contracts.Common.Events;

namespace HxcApi.Events.Common.EventPublishers;

public abstract class EventPublisher<TEvent> : IEventPublisher<TEvent>
    where TEvent : IEvent
{
    public async Task PublishAsync(TEvent eventToPublish)
    {
        await OnBeforePublishingEventAsync(eventToPublish);
    }

    protected virtual Task OnBeforePublishingEventAsync(TEvent eventToPublish)
    {
        return Task.CompletedTask;
    }
}