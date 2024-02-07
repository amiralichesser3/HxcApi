using HxcApi.Common.Persistence.Commands;
using HxcApi.Todos.Contracts.Commands;
using HxcApi.Todos.Contracts.Queries;
using HxcApi.Todos.Contracts.SqlQueries;
using HxcApi.Todos.Persistence.Commands.CommandHandlers;
using HxcApi.Todos.Persistence.Commands.CommandValidators;
using HxcApi.Todos.Persistence.Queries.QueryGenerators;
using HxcApi.Todos.Persistence.Queries.QueryHandlers;

namespace HxcApi.Todos.Persistence.Ioc;

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