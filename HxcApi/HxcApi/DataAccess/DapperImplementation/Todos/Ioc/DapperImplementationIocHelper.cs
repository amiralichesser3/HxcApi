using HxcApi.DataAccess.Contracts.Common.Queries;
using HxcApi.DataAccess.Contracts.Todos.Queries;
using HxcApi.DataAccess.DapperImplementation.Todos.Queries;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Ioc;

public static class DapperImplementationIocHelper
{
    public static void RegisterTodoQueries(this IServiceCollection services)
    {
        services.AddScoped<IGetOrganizationTodosQueryGenerator, GetOrganizationTodosQueryGenerator>();
        services.AddScoped<IGetOrganizationTodosQueryHandler, GetOrganizationTodosQueryHandler>(); }
}