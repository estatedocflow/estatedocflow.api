using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace estatedocflow.api.RabbitMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",     
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
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
            _connection.Close();
        }

        public void SendMessage<T>(T message)
        {
            var body = Encoding.UTF8.GetBytes("server processed " + message);

            // Use polly to enable retries
            var retry = Policy
                .Handle<Exception>()
                .WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            // Publish the message to the queue
            retry.Execute(() =>
                _channel.BasicPublish(
                    exchange: "",
                    routingKey: "myQueue",
                    basicProperties: null,
                    body: body
                )
            );

            Console.WriteLine(" [x] Published {0} to RabbitMQ", message);
        }        
    }
}
