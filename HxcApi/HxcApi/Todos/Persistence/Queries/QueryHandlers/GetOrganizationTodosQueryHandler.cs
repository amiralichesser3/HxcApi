using Dapper;
using HxcApi.Common.Persistence.Queries;
using HxcApi.Todos.Contracts.Queries;
using HxcApi.Todos.Contracts.SqlQueries;
using HxcCommon;

namespace HxcApi.Todos.Persistence.Queries.QueryHandlers;

public class GetOrganizationTodosQueryHandler(IServiceProvider serviceProvider, ITodosSqlQueryGenerator sqlQueryGenerator) :
    QueryHandler<GetOrganizationTodosQuery, IEnumerable<Todo>>(serviceProvider),
    IGetOrganizationTodosQueryHandler
{
    public override async Task<IEnumerable<Todo>> Handle(GetOrganizationTodosQuery query)
    {
        string queryText;
        if (query.TodoId is not null)
        {
            queryText = sqlQueryGenerator.GenerateSelectByIdQuery();
            return await SqlConnection.QueryAsync<Todo>(queryText, new { Id = query.TodoId.Value });
        }
        queryText = sqlQueryGenerator.GenerateSelectAllQuery();
        return await SqlConnection.QueryAsync<Todo>(queryText);
    }
}