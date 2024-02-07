using HxcCommon;

namespace HxcApi.Todos.Exceptions;

public class TodoException(Guid todoExceptionId, string target) : KnownException
{
    private Guid TodoExceptionId { get; } = todoExceptionId;
    public override string Message => "Todo is invalid";
    public override string KnownExceptionParams => TodoExceptionId.ToString();
    public override string Target => target;
}