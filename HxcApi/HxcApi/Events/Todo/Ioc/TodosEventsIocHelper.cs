using HxcApi.DataAccess.Contracts.Common.Events;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.Events.Todo.EventHandlers;
using HxcApi.Events.Todo.EventPublishers;

namespace HxcApi.Events.Todo.Ioc;

public static class TodosEventsIocHelper
{
    public static void RegisterTodoEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher<CreateOrganizationTodoCommand>, CreateOrganizationTodoEventPublisher>();
        services.AddScoped<IEventHandler<CreateOrganizationTodoCommand>, CreateOrganizationTodoEventHandler>();
    }
}