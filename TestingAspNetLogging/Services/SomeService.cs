using Microsoft.Extensions.Logging;
using System;

namespace TestingAspNetLogging.Services
{
    public class SomeService
    {
        private readonly ILogger<SomeService> _logger;

        public SomeService(ILogger<SomeService> logger)
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
