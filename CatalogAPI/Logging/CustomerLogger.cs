using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace CatalogAPI.Logging
{
    public class CustomerLogger : ILogger
    {
        private readonly string loggerName;
        private readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            WriteTextIntoFile(message);
        }

        private void WriteTextIntoFile(string message)
        {
            string rootPath = new DirectoryInfo(".").FullName;

            DirectoryInfo filesDirectoryInfo = new DirectoryInfo(Path.Join(rootPath, "Files"));

            if (!filesDirectoryInfo.Exists)
                filesDirectoryInfo.Create();

            string fileLogPath = Path.Join(filesDirectoryInfo.FullName, "log.txt");

            using StreamWriter streamWriter = new StreamWriter(fileLogPath, true);

            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
