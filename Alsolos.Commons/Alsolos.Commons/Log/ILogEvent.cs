namespace Alsolos.Commons.Log
{
    using System;

    public interface ILogEvent
    {
        DateTime Timestamp { get; }

        LogLevel Level { get; }

        string Message { get; }
    }
}
