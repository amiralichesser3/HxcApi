using HxcApiClient.Common;
using HxcApiClient.Todos;
using Microsoft.Extensions.DependencyInjection;

namespace HxcApiClient.Ioc;

public static class ServiceExtensions
{
    public static void SetupHxcApiClient(this IServiceCollection services)
    {
        services.AddScoped<ITodosClient, TodosClient>();
        services.AddScoped<IHxcApiClient, HxcApiHttpClient>();
    }
}