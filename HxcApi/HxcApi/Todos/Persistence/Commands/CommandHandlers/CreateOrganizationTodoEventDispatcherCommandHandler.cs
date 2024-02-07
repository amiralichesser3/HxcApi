using Dapper;
using HxcApi.Common.Persistence.Commands;
using HxcApi.Todos.Contracts.Commands;
using HxcApi.Todos.Contracts.SqlQueries;

namespace HxcApi.Todos.Persistence.Commands.CommandHandlers;

public class CreateOrganizationTodoCommandHandler(
    IServiceProvider serviceProvider,
    ITodosSqlQueryGenerator todosSqlQueryGenerator)
    : EventDispatchingCommandHandler<CreateOrganizationTodoCommand>(serviceProvider), ICreateOrganizationTodoCommandHandler
{
    protected override async Task Handle(CreateOrganizationTodoCommand command)
    {
        string query = todosSqlQueryGenerator.GenerateInsertQuery();    
        await WriteSqlConnection.ExecuteAsync(query, command.Todo);
    }
}