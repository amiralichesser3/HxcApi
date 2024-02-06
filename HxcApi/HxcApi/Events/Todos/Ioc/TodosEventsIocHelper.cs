using HxcApi.DataAccess.Contracts.Common.Events;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.Events.Todos.EventHandlers;
using HxcApi.Events.Todos.EventPublishers;

namespace HxcApi.Events.Todos.Ioc;

public static class TodosEventsIocHelper
{
    public static void RegisterTodoEvents(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher<CreateOrganizationTodoCommand>, CreateOrganizationTodoEventPublisher>();
        services.AddScoped<IEventHandler<CreateOrganizationTodoCommand>, CreateOrganizationTodoEventHandler>();
    }
}