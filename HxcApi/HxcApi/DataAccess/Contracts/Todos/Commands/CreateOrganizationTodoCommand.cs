using HxcApi.DataAccess.Contracts.Common.Commands;
using HxcApi.DataAccess.Contracts.Common.Events;
using HxcCommon;

namespace HxcApi.DataAccess.Contracts.Todos.Commands;

public class CreateOrganizationTodoCommand(Todo todo) : Event, ICommand
{
    public Todo Todo { get; set; } = todo;
}