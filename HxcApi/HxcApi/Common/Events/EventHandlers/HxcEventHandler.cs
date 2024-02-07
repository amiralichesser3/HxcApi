using HxcApi.Common.Events.Contracts;
using Microsoft.Data.SqlClient;

namespace HxcApi.Common.Events.EventHandlers;

public abstract class HxcEventHandler<T>(IServiceProvider serviceProvider) : IEventHandler<T> where T : Event
{
    protected readonly SqlConnection SqlConnection = serviceProvider.GetKeyedService<SqlConnection>("ReadSqlConnection")!;
    public abstract Task HandleAsync(T eventToHandle);
}