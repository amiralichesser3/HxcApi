namespace HxcApi.DataAccess.Contracts.Common.Events;

public interface IEvent
{
    Guid Id { get; set; }
    DateTimeOffset CreatedDateTime { get; set; }
    DateTimeOffset? PublishedDateTime { get; set; }
}