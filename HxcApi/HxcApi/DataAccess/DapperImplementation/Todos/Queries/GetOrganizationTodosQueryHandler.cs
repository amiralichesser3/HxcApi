using Dapper;
using HxcApi.DataAccess.Contracts.Common.Queries;
using HxcApi.DataAccess.Contracts.Todos.Queries;
using HxcApi.DataAccess.Contracts.Todos.SqlQueries;
using HxcCommon;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Queries;

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