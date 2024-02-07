namespace HxcApi.Common.Persistence.SqlQueries;

public interface ISqlQueryGenerator
{
    string GetQuery(string tagName);
}