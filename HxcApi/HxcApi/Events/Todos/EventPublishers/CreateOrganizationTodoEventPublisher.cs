using System.Text.Json;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.Events.Common.EventPublishers;
using HxcApi.Utility;

namespace HxcApi.Events.Todos.EventPublishers;

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