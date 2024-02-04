using HxcApi.DataAccess.Contracts.Common.Events;
using HxcApi.DataAccess.Contracts.Todos.Commands;

namespace HxcApi.Events.Todo.EventHandlers;

public class CreateOrganizationTodoEventHandler : IEventHandler<CreateOrganizationTodoCommand>
{
    public Task HandleAsync(CreateOrganizationTodoCommand eventToHandle)
    {
        throw new NotImplementedException();
    }
}