using HxcApi.DataAccess.Contracts.Todos.Commands;
using HxcApi.Events.Common.EventPublishers;

namespace HxcApi.Events.Todo.EventPublishers;

public class CreateOrganizationTodoEventPublisher : EventPublisher<CreateOrganizationTodoCommand>;