using Microsoft.Extensions.Logging;
using System;

namespace TestingAspNetLogging.Services
{
    public interface ILoggerAdapter<T>
    {
        // add just the logger methods your app uses
        void LogInformation(string message);
        void LogError(Exception ex, string message, params object[] args);
    }
    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.LogError(ex, message, args);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
    }
    public class SomeOtherService
    {
        private readonly ILoggerAdapter<SomeOtherService> _logger;

        public SomeOtherService(ILoggerAdapter<SomeOtherService> logger)
        {
            _logger = logger;
        }
        public void DoSomething(int input)
        {
            _logger.LogInformation("Doing something...");

            try
            {
                // do something that might result in an exception
                var result = 10 / input;

            }
            catch (Exception ex)
            {
                // swallow but log the exception
                _logger.LogError(ex, "An error occurred doing something.", input);
            }
        }
    }
}
