using Dapper;
using HxcApi.Common.Events.EventHandlers;
using HxcApi.Todos.Contracts.Commands;
using HxcApi.Todos.Contracts.SqlQueries;

namespace HxcApi.Todos.Events.EventHandlers;

public class CreateOrganizationTodoEventHandler(IServiceProvider serviceProvider, ITodosSqlQueryGenerator todosSqlQueryGenerator)
    : HxcEventHandler<CreateOrganizationTodoCommand>(serviceProvider)
{
    public override async Task HandleAsync(CreateOrganizationTodoCommand eventToHandle)
    {
        string query = todosSqlQueryGenerator.GenerateInsertQuery();
        await SqlConnection.ExecuteAsync(query, eventToHandle.Todo);
    }
}