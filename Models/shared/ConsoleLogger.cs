using System;
using Microsoft.Extensions.Logging;
using static System.Console;
#nullable disable
namespace Shopy.Models.Shared;
public class ConsoleLoggerProvider : ILoggerProvider
{
    public void Dispose() { }
    public ILogger CreateLogger(string planType)
    {
        return new ConsoleLogger();
    }

}
public class ConsoleLogger : ILogger
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }
    public bool IsEnabled(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
            case LogLevel.Information:
            case LogLevel.None:
                return false;
            case LogLevel.Debug:
            case LogLevel.Warning:
            case LogLevel.Error:
            case LogLevel.Critical:
            default:
                return true;
        };
    }
#nullable enable
    public void Log<TState>(LogLevel logLevel,
    EventId eventId, TState state, Exception? exception,
    Func<TState, Exception, string> formatter)
    {
        if (eventId.Id == 20100)
        {
            // log the level and event identifier
            Write($"Level: {logLevel}, Event Id: {eventId.Id}");
            // only output the state or exception if it exists
            if (state != null)
            {
                Write($", State: {state}");
            }
            if (exception != null)
            {
                Write($", Exception: {exception.Message}");
            }
            WriteLine();
        }
    }
}
