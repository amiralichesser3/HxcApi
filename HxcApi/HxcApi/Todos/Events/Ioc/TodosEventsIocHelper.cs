using HxcApi.Common.Events.Contracts;
using HxcApi.Todos.Contracts.Commands;
using HxcApi.Todos.Events.EventHandlers;
using HxcApi.Todos.Events.EventPublishers;

namespace HxcApi.Todos.Events.Ioc;

public static class TodosEventsIocHelper
{
    public static void RegisterTodoEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher<CreateOrganizationTodoCommand>, CreateOrganizationTodoEventPublisher>();
        services.AddScoped<IEventHandler<CreateOrganizationTodoCommand>, CreateOrganizationTodoEventHandler>();
    }
}