namespace HxcCommon;

public abstract class KnownException : Exception
{
    public abstract string KnownExceptionParams { get; }
    public abstract string Target { get; }
}