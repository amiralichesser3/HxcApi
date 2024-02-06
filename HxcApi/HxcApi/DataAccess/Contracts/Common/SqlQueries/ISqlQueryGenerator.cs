namespace HxcApi.DataAccess.Contracts.Common.SqlQueries;

public interface ISqlQueryGenerator
{
    string GetQuery(string tagName);
}