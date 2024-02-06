using Dapper;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.DataAccess.Contracts.Todos.SqlQueries;
using HxcApi.Events.Common.EventHandlers;

namespace HxcApi.Events.Todos.EventHandlers;

public class CreateOrganizationTodoEventHandler(IServiceProvider serviceProvider, ITodosSqlQueryGenerator todosSqlQueryGenerator)
    : HxcEventHandler<CreateOrganizationTodoCommand>(serviceProvider)
{
    public override async Task HandleAsync(CreateOrganizationTodoCommand eventToHandle)
    {
        string query = todosSqlQueryGenerator.GenerateInsertQuery();
        await SqlConnection.ExecuteAsync(query, eventToHandle.Todo);
    }
}