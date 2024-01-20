using HxcApi.DataAccess.Contracts.Common.Queries;
using HxcCommon;

namespace HxcApi.DataAccess.Contracts.Todos.Queries;

public class GetOrganizationTodosQuery : IQuery<IEnumerable<Todo>>
{
    public int? TodoId { get; set; }
}