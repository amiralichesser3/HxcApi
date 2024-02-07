using HxcCommon;

namespace HxcApi.Todos.Exceptions;

public class CannotKillAnybodyException(string target) : KnownException
{
    public override string Message => "You cannot kill anybody.";
    public override string KnownExceptionParams => "";
    public override string Target => target;
}