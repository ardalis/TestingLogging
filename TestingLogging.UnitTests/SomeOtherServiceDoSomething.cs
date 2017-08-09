using Microsoft.Extensions.Logging;
using Moq;
using System;
using TestingAspNetLogging.Services;
using Xunit;

namespace TestingLogging.UnitTests
{
    public class SomeOtherServiceDoSomething
    {
        [Fact]
        public void LogsErrorWhenInputIsZero()
        {
            var mockLogger = new Mock<ILoggerAdapter<SomeOtherService>>();
            var someOtherService = new SomeOtherService(mockLogger.Object);

            someOtherService.DoSomething(0);

            mockLogger.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>(), 0));
        }
    }
}
