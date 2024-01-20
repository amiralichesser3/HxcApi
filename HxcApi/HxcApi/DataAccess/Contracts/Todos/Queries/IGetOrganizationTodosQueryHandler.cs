using HxcApi.DataAccess.Contracts.Common.Queries;
using HxcCommon;

namespace HxcApi.DataAccess.Contracts.Todos.Queries;

public interface IGetOrganizationTodosQueryHandler : IQueryHandler<GetOrganizationTodosQuery, IEnumerable<Todo>>;