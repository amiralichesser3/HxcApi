using HxcApi.Common.Persistence.Queries;
using HxcCommon;

namespace HxcApi.Todos.Contracts.Queries;

public class GetOrganizationTodosQuery : IQuery<IEnumerable<Todo>>
{
    public int? TodoId { get; set; }
}