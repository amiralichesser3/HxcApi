using System.Text.Json;
using HxcApi.Common.Events.EventPublishers;
using HxcApi.Todos.Contracts.Commands;

namespace HxcApi.Todos.Events.EventPublishers;

public class CreateOrganizationTodoEventPublisher(RabbitMqService rabbitMqService) : EventPublisher<CreateOrganizationTodoCommand>
{
    protected override Task Publish(CreateOrganizationTodoCommand eventToPublish)
    {
        JsonSerializerOptions sourceGenOptions = new JsonSerializerOptions
        {
            TypeInfoResolver = new AppJsonSerializerContext()
        };

        string jsonString =
            JsonSerializer.Serialize(eventToPublish, typeof(CreateOrganizationTodoCommand), sourceGenOptions);
        rabbitMqService.Send(jsonString);
        return Task.CompletedTask;
    }
}