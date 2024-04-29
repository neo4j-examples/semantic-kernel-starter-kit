using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using Config = Neo4jExamples.SemanticKernel.Config;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Neo4jLogger = Neo4j.Driver.ILogger;

namespace Neo4jExamples.SemanticKernel;

sealed class DriverFactory
{
    public static IDriver BuildDriver(ILoggerFactory loggerFactory)
    {
        return GraphDatabase.Driver(Config.Neo4jUri, AuthTokens.Basic(Config.Neo4jUser, Config.Neo4jPassword), x => x.WithLogger(new Logger(loggerFactory.CreateLogger("driver"))));
    }

    private sealed class Logger(ILogger logger) : Neo4jLogger
    {
        public void Error(Exception cause, string message, params object[] args)
        {
            if (cause != null)
                logger.Log(LogLevel.Error, cause, message, args);
            else
                logger.Log(LogLevel.Error, message, args);
        }

        public void Warn(Exception cause, string message, params object[] args)
        {
            if (cause != null)
                logger.Log(LogLevel.Warning, cause, message, args);
            else
                logger.Log(LogLevel.Warning, message, args);
        }

        public void Info(string message, params object[] args)
        {
            logger.Log(LogLevel.Information, message, args);
        }

        public void Debug(string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        public void Trace(string message, params object[] args)
        {
            // NoOp
        }

        public bool IsTraceEnabled() => false;
        public bool IsDebugEnabled() => true;
    }
}