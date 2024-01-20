using Microsoft.Data.SqlClient;

namespace HxcApi.DataAccess.Contracts.Common.Queries;

public abstract class QueryHandler<T, TY>(SqlConnection sqlConnection)
    : IQueryHandler<T, TY>
    where T : IQuery<TY>
{
    protected readonly SqlConnection SqlConnection = sqlConnection;
    public abstract Task<TY> Handle(T query);
}