using HxcApiClient.Http;
using HxcApiClient.Records;

namespace HxcApiClient.Todos;

public interface ITodosClient
{
    Task<HxcHttpResponse<ICollection<TodoRecord>>> GetUserTodosAsync();
    Task<HxcHttpResponse<ICollection<TodoRecord>>> GetOrganizationTodosAsync();
}