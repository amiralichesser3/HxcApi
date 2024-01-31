using HxcApi.DataAccess.Contracts.Common.Commands;
using HxcCommon;

namespace HxcApi.DataAccess.Contracts.Todos.Commands;

public class CreateOrganizationTodoCommand(Todo todo) : ICommand
{
    public Todo Todo { get; set; } = todo;
}