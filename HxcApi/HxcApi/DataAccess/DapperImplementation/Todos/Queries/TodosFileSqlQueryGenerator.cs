using HxcApi.DataAccess.Contracts.Todos.SqlQueries;
using HxcApi.DataAccess.DapperImplementation.Common;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Queries;

public class TodosFileSqlQueryGenerator : FileSqlQueryGenerator, ITodosSqlQueryGenerator
{
    public string GenerateSelectAllQuery()
    {
        return GetQuery("GetTodos");
    }
    
    public string GenerateSelectByIdQuery()
    {
        return GetQuery("GetTodosById");
    }

    public string GenerateInsertQuery()
    {
        return GetQuery("InsertTodo");
    }
}