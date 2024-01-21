using HxcApi.DataAccess.Contracts.Common.Queries;
using HxcApi.DataAccess.Contracts.Todos.Queries;
using HxcApi.DataAccess.DapperImplementation.Todos.Queries;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Ioc;

public static class TodosIocHelper
{
    public static void RegisterTodoServices(this IServiceCollection services)
    {
        services.AddScoped<IGetOrganizationTodosQueryHandler, GetOrganizationTodosQueryHandler>();
        services.AddSingleton<IGetOrganizationTodosQueryGenerator, GetOrganizationTodosQueryGenerator>();
    }
}