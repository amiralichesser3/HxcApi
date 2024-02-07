namespace HxcApi.Common.Events.Contracts;

public abstract class Event : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? PublishedDateTime { get; set; }
}