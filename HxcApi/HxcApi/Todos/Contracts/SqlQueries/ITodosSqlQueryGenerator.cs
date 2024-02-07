using HxcApi.Common.Persistence.SqlQueries;

namespace HxcApi.Todos.Contracts.SqlQueries;

public interface ITodosSqlQueryGenerator : ISqlQueryGenerator
{
    string GenerateSelectAllQuery();
    string GenerateSelectByIdQuery();
    string GenerateInsertQuery();
}