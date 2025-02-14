using FluentAssertions;
using Xunit;

namespace Serilog.TestLogger.Tests;

public class SerilogTestLoggerTests
{
    [Fact]
    public void WhenExtendedWithSink_LogsShouldBeWrittenToAllSinks()
    {
        // Arrange
        var stubSink = new SerilogTestSink();
        using var testLogger = new SerilogTestLogger(o => o.WriteTo.Sink(stubSink));
        
        var logMessage = "This log should appear in stubSink";
        
        // Act
        testLogger.Error(logMessage);

        // Assert
        stubSink.Events.Should().ContainSingle().Which.MessageTemplate.Text.Should().Be(logMessage);
        testLogger.Events.Should().ContainSingle().Which.MessageTemplate.Text.Should().Be(logMessage);
    }
}