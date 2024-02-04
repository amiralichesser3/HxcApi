using Dapper;
using HxcApi.DataAccess.Contracts.Common.Commands;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.DataAccess.Contracts.Todos.SqlQueries;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Commands;

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