using Dapper;
using HxcApi.DataAccess.Contracts.Common.Queries;
using HxcApi.DataAccess.Contracts.Todos.Queries;
using HxcCommon;
using Microsoft.Data.SqlClient;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Queries;

public class GetOrganizationTodosQueryHandler(SqlConnection sqlConnection, IGetOrganizationTodosQueryGenerator queryGenerator) :
    QueryHandler<GetOrganizationTodosQuery, IEnumerable<Todo>>(sqlConnection),
    IGetOrganizationTodosQueryHandler
{
    public override async Task<IEnumerable<Todo>> Handle(GetOrganizationTodosQuery query)
    {
        var queryText = query.TodoId is not null ?
            queryGenerator.GenerateSelectByIdQuery(query.TodoId.Value) :
            queryGenerator.GenerateSelectAllQuery();

        return await SqlConnection.QueryAsync<Todo>(queryText);
    }
}