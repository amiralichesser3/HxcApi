namespace HxcApi.Common.Events.Contracts;

public interface IEvent
{
    Guid Id { get; set; }
    DateTimeOffset CreatedDateTime { get; set; }
    DateTimeOffset? PublishedDateTime { get; set; }
}