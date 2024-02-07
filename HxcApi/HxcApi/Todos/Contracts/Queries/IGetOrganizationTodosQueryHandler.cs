using HxcApi.Common.Persistence.Queries;
using HxcCommon;

namespace HxcApi.Todos.Contracts.Queries;

public interface IGetOrganizationTodosQueryHandler : IQueryHandler<GetOrganizationTodosQuery, IEnumerable<Todo>>;