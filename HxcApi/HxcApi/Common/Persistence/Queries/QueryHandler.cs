using Microsoft.Data.SqlClient;

namespace HxcApi.Common.Persistence.Queries;

public abstract class QueryHandler<T, TY>(IServiceProvider serviceProvider)
    : IQueryHandler<T, TY>
    where T : IQuery<TY>
{
    protected readonly SqlConnection SqlConnection = serviceProvider.GetKeyedService<SqlConnection>("ReadSqlConnection")!;
    public abstract Task<TY> Handle(T query);
}