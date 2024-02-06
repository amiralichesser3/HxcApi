using HxcApi.DataAccess.Contracts.Common.Events;
using Microsoft.Data.SqlClient;

namespace HxcApi.Events.Common.EventHandlers;

public abstract class HxcEventHandler<T>(IServiceProvider serviceProvider) : IEventHandler<T> where T : Event
{
    protected readonly SqlConnection SqlConnection = serviceProvider.GetKeyedService<SqlConnection>("ReadSqlConnection")!;
    public abstract Task HandleAsync(T eventToHandle);
}