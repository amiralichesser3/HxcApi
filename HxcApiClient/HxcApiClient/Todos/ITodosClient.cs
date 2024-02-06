using HxcApiClient.Http;
using HxcApiClient.Todos.Records;

namespace HxcApiClient.Todos;

public interface ITodosClient
{
    Task<HxcHttpResponse<ICollection<TodoRecord>>> GetUserTodosAsync();
    Task<HxcHttpResponse<ICollection<TodoRecord>>> GetOrganizationTodosAsync();
    Task<HxcHttpResponse<TodoRecord>> GetOrganizationTodoByIdAsync(int id);
    Task<HxcHttpResponse> CreateOrganizationTodoAsync(TodoRecord todo);
}