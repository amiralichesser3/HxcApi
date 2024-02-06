using HxcCommon;

namespace HxcApi.ExceptionHandling.Todo;

public class CannotKillAnybodyException(string target) : KnownException
{
    public override string Message => "You cannot kill anybody.";
    public override string KnownExceptionParams => "";
    public override string Target => target;
}