using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace CatalogAPI.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration loggerConfig;
        private readonly ConcurrentDictionary<string, CustomerLogger> loggers =
            new ConcurrentDictionary<string, CustomerLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            loggerConfig = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName,
                name => new CustomerLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
