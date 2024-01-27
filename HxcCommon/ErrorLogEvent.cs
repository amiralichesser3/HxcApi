using System.Diagnostics;

namespace HxcCommon;

public class ErrorLogEvent
{
    public int Id { get; set; }
    public string Exception { get; set; } = "";
    public string Message { get; set; } = "";
    public string Level { get; set; } = "";
    public string Timestamp { get; set; } = "";
    public string ClassName { get; set; } = "";
    public int LineNumber { get; set; }
    public string KnownExceptionParams { get; set; } = "";
    public string AppVersion { get; set; } = "";
    public string? StackTrace { get; set;}
    
    public static ErrorLogEvent GenerateFromException(Exception exception, string level, string appVersion)
    {
        var logEvent = new ErrorLogEvent
        {
            Exception = exception.GetType().FullName!,
            Message = exception.Message,
            Level = level,
            StackTrace = exception.StackTrace,
            Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            AppVersion = appVersion
        };

        AppendStacktraceInfo(exception, logEvent);

        return logEvent;
    }

    private static void AppendStacktraceInfo(Exception ex, ErrorLogEvent logEvent)
    {
            var st = new StackTrace(ex, true);

            var frame = st.GetFrame(0);

            if (frame != null)
            {
                var line = frame.GetFileLineNumber();
                logEvent.LineNumber = line;
            }

            if (ex is KnownException exception)
            {
                logEvent.KnownExceptionParams = exception.KnownExceptionParams;
                logEvent.ClassName = exception.Target;
            }
            else if (ex.StackTrace != null)
            {
                logEvent.ClassName = GetClassNameFromStackTrace(ex.StackTrace);
            }
    }

    private static string GetClassNameFromStackTrace(string stackTrace)
    {
        string[] lines = stackTrace.Split('\n');
        string firstLine = lines[0];
        int firstIndex = firstLine.IndexOf(" in ", StringComparison.Ordinal);
        if (firstIndex <= 0)
        {
            return firstLine;
        }
        int secondIndex = firstLine.IndexOf(":line", StringComparison.Ordinal);
        string className = firstLine.Substring(firstIndex + 4, secondIndex - firstIndex - 4);
        return className;
    }
}