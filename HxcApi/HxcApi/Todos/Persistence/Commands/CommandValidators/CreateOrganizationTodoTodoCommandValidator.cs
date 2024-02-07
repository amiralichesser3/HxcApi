using HxcApi.Common.Persistence.Commands;
using HxcApi.Todos.Contracts.Commands;
using HxcApi.Todos.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace HxcApi.Todos.Persistence.Commands.CommandValidators;

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