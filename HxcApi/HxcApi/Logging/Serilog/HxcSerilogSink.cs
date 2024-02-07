using Dapper;
using HxcCommon;
using Microsoft.Data.SqlClient;
using Serilog.Core;
using Serilog.Events;

namespace HxcApi.Logging.Serilog;

public class HxcSerilogSink(string connectionString, string appVersion) : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        if (logEvent.Exception == null) return;
        ErrorLogEvent errorLogEvent =
            ErrorLogEvent.GenerateFromException(logEvent.Exception, logEvent.Level.ToString(), appVersion);
        string command = @"INSERT INTO ErrorLogEvents (Exception, Message, Level, Timestamp, ClassName, LineNumber, KnownExceptionParams, AppVersion, StackTrace)
            VALUES (@Exception, @Message, @Level, @Timestamp, @ClassName, @LineNumber, @KnownExceptionParams, @AppVersion, @StackTrace)";

        using var connection = new SqlConnection(connectionString);
        connection.Open();
        connection.Execute(command, errorLogEvent);
    }
}