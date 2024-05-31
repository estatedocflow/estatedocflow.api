using estatedocflow.api.RabbitMQ;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;

namespace estatedocflow.api.tests
{
    public class RabbitMqServiceTests
    {
        private readonly Mock<IConnection> _mockConnection;
        private readonly Mock<IModel> _mockChannel;
        private readonly Mock<ILogger<RabbitMqService>> _mockLogger;
        private readonly RabbitMqService _rabbitMqService;

        public RabbitMqServiceTests()
        {
            _mockConnection = new Mock<IConnection>();
            _mockChannel = new Mock<IModel>();
            _mockLogger = new Mock<ILogger<RabbitMqService>>();

            _mockConnection.Setup(c => c.CreateModel()).Returns(_mockChannel.Object);

            _rabbitMqService = new RabbitMqService(_mockConnection.Object, _mockLogger.Object);
        }

        [Fact]
        public void SendMessage_ShouldLogInformation()
        {
            // Arrange
            var testMessage = "Test message";

            // Act
            _rabbitMqService.SendMessage(testMessage);

            // Assert
            _mockChannel.Verify(c => c.BasicPublish(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IBasicProperties>(),
                It.IsAny<byte[]>()), Times.Once);

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Published message")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public void CloseConnection_ShouldCallChannelClose()
        {
            // Act
            _rabbitMqService.CloseConnection();

            // Assert
            _mockChannel.Verify(c => c.Close(), Times.Once);
            _mockConnection.Verify(c => c.Close(), Times.Once);
        }
    }
}
