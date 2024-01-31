using HxcApi.DataAccess.Contracts.Common.Commands;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.DataAccess.DapperImplementation.Todos.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Commands;

public class CreateOrganizationTodoCommandValidator : ICommandValidator<CreateOrganizationTodoCommand>
{
    public Task ValidateAsync(CreateOrganizationTodoCommand command)
    {
        if (command.Todo.Title.IsNullOrEmpty())
        {
            throw new MissingFieldException(nameof(CreateOrganizationTodoCommand), nameof(CreateOrganizationTodoCommand.Todo.Title));
        }
        if (command.Todo.Title!.StartsWith("killing", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new CannotKillAnybodyException(nameof(CreateOrganizationTodoCommandValidator));
        }

        return Task.CompletedTask;
    }
}