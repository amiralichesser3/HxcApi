using HxcApi.Common.Events.Contracts;
using HxcApi.Common.Persistence.Commands;
using HxcCommon;

namespace HxcApi.Todos.Contracts.Commands;

public class CreateOrganizationTodoCommand(Todo todo) : Event, ICommand
{
    public Todo Todo { get; set; } = todo;
}