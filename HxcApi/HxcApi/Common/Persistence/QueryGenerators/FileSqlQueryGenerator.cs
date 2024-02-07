using HxcApi.Common.Persistence.SqlQueries;
using YeSql.Net;

namespace HxcApi.Common.Persistence.QueryGenerators;

public class FileSqlQueryGenerator : ISqlQueryGenerator
{
    public string GetQuery(string tagName)
    {
        ISqlCollection sqlStatements = new YeSqlLoader().LoadFromDefaultDirectory();
        return sqlStatements[tagName];
    }
}