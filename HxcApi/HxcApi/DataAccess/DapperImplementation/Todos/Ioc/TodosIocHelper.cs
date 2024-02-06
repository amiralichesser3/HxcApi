using HxcApi.DataAccess.Contracts.Common.Commands;
using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.DataAccess.Contracts.Todos.Queries;
using HxcApi.DataAccess.Contracts.Todos.SqlQueries;
using HxcApi.DataAccess.DapperImplementation.Todos.Commands;
using HxcApi.DataAccess.DapperImplementation.Todos.Queries;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Ioc;

public static class TodosIocHelper
{
    public static void RegisterTodoServices(this IServiceCollection services)
    {
        services.AddScoped<IGetOrganizationTodosQueryHandler, GetOrganizationTodosQueryHandler>();
        services.AddScoped<ICreateOrganizationTodoCommandHandler, CreateOrganizationTodoCommandHandler>();
        services.AddSingleton<ITodosSqlQueryGenerator, TodosFileSqlQueryGenerator>();
        services.AddScoped<ICommandValidator<CreateOrganizationTodoCommand>, CreateOrganizationTodoCommandValidator>();
    }
}