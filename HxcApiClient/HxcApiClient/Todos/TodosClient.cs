using HxcApiClient.Http;
using HxcApiClient.Records;

namespace HxcApiClient.Todos;

internal class TodosClient(HttpClient hxcHttpClient) : BaseClient, ITodosClient
{
    private const string UserTodosAddress = "/api/user/todos";
    private const string OrganizationTodosAddress = "/api/organization/todos";

    public async Task<HxcHttpResponse<ICollection<TodoRecord>>> GetUserTodosAsync()
    {
        HttpResponseMessage response = await hxcHttpClient
            .GetAsync(UserTodosAddress).ConfigureAwait(false);

        return await HandleResponse<ICollection<TodoRecord>>(response);
    }

    public async Task<HxcHttpResponse<ICollection<TodoRecord>>> GetOrganizationTodosAsync()
    {
        HttpResponseMessage response = await hxcHttpClient
            .GetAsync(OrganizationTodosAddress).ConfigureAwait(false);

        return await HandleResponse<ICollection<TodoRecord>>(response);
    }

    public async Task<HxcHttpResponse<TodoRecord>> GetOrganizationTodoByIdAsync(int todoId)
    {
        HttpResponseMessage response = await hxcHttpClient
            .GetAsync($"{OrganizationTodosAddress}/{todoId}").ConfigureAwait(false);

        return await HandleResponse<TodoRecord>(response);
    }
}
