using HxcApiClient.Todos;

namespace HxcApiClient.Common;

internal class HxcApiHttpClient(ITodosClient todos) : IHxcApiClient
{
    public ITodosClient Todos { get; } = todos;
}