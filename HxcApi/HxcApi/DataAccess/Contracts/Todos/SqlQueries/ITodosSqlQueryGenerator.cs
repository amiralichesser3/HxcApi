using HxcApi.DataAccess.Contracts.Common.SqlQueries;

namespace HxcApi.DataAccess.Contracts.Todos.SqlQueries;

public interface ITodosSqlQueryGenerator : ISqlQueryGenerator
{
    string GenerateSelectAllQuery();
    string GenerateSelectByIdQuery();
    string GenerateInsertQuery();
}