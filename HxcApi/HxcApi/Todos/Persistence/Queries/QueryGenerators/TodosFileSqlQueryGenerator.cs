using HxcApi.Common.Persistence.QueryGenerators;
using HxcApi.Todos.Contracts.SqlQueries;

namespace HxcApi.Todos.Persistence.Queries.QueryGenerators;

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