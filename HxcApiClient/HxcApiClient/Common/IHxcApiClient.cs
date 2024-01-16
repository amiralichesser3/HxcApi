using HxcApiClient.Todos;

namespace HxcApiClient.Common;

public interface IHxcApiClient
{
    ITodosClient Todos { get; }
}