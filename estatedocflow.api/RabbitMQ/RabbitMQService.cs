using Polly;
using RabbitMQ.Client;
using System.Text;

namespace estatedocflow.api.RabbitMQ
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IModel _channel;
        private readonly IConnection _rabbitMqConnection;
        private readonly ILogger<RabbitMqService> _logger;

        public RabbitMqService(IConnection rabbitMqConnection, ILogger<RabbitMqService> logger)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _channel = _rabbitMqConnection.CreateModel();
            _logger = logger;
            _channel.QueueDeclare(
                queue: "myQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        public void CloseConnection()
        {
            _channel.Close();
            _rabbitMqConnection.Close();
        }

        public void SendMessage<T>(T message)
        {
            var body = Encoding.UTF8.GetBytes("server processed " + message);

            // Use Polly to enable retries
            var retry = Policy
                .Handle<Exception>()
                .WaitAndRetry(2, retryAttempt =>
                {
                    var waitTime = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    _logger.LogWarning("Retrying in {Time} seconds due to an error.", waitTime.TotalSeconds);
                    return waitTime;
                });

            // Publish the message to the queue
            retry.Execute(() =>
            {
                _channel.BasicPublish(
                    exchange: "",
                    routingKey: "myQueue",
                    basicProperties: null,
                    body: body
                );
                _logger.LogInformation("Published message: {Message} to RabbitMQ", message);
            });
        }
    }
}