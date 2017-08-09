using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using TestingAspNetLogging.Services;
using Xunit;

namespace TestingLogging.UnitTests
{
    public class SomeServiceDoSomething
    {
        [Fact]
        public void LogsErrorWhenInputIsZero()
        {
            var mockLogger = new Mock<ILogger<SomeService>>();
            var someService = new SomeService(mockLogger.Object);

            someService.DoSomething(0);

            // Option 1: Try to verify the actual code that was called.
            // Doesn't work.
            mockLogger.Verify(l => l.LogCritical(It.IsAny<Exception>(), It.IsAny<string>(), 0));
        }

        [Fact]
        public void LogsErrorWhenInputIsZeroTake2()
        {
            var mockLogger = new Mock<ILogger<SomeService>>();
            var someService = new SomeService(mockLogger.Object);

            someService.DoSomething(0);

            // Option 2: Look up what instance method the extension method actually calls:
            // https://github.com/aspnet/Logging/blob/dev/src/Microsoft.Extensions.Logging.Abstractions/LoggerExtensions.cs#L342
            // Mock the underlying call instead.
            // Works but is ugly and brittle
            mockLogger.Verify(l => l.Log(LogLevel.Error, 0, It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()));
        }

        [Fact]
        public void LogsErrorWhenInputIsZeroTake3()
        {
            var fakeLogger = new FakeLogger();
            var someService = new SomeService(fakeLogger);

            someService.DoSomething(0);

            // Option 3: Create your own instance of ILogger<T> that has a non-extension version of the method
            // Doesn't work.
            Assert.NotNull(FakeLogger.ProvidedException);
            Assert.NotNull(FakeLogger.ProvidedMessage);
        }

        private class FakeLogger : ILogger<SomeService>
        {
            public static Exception ProvidedException { get; set; }
            public static string ProvidedMessage { get; set; }
            public static object[] ProvidedArgs { get; set; }
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
            }

            public void LogError(Exception ex, string message, params object[] args)
            {
                ProvidedException = ex;
                ProvidedMessage = message;
                ProvidedArgs = args;
            }
        }
    }
}
