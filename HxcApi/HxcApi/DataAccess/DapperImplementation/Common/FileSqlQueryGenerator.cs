using HxcApi.DataAccess.Contracts.Common.SqlQueries;
using YeSql.Net;

namespace HxcApi.DataAccess.DapperImplementation.Common;

public class FileSqlQueryGenerator : ISqlQueryGenerator
{
    public string GetQuery(string tagName)
    {
        ISqlCollection sqlStatements = new YeSqlLoader().LoadFromDefaultDirectory();
        return sqlStatements[tagName];
    }
}