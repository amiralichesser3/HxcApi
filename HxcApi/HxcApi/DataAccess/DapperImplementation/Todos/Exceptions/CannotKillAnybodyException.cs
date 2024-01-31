using HxcCommon;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Exceptions;

public class CannotKillAnybodyException(string target) : KnownException
{
    public override string Message => "You cannot kill anybody.";
    public override string KnownExceptionParams => "";
    public override string Target => target;
}