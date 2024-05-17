namespace estatedocflow.api.RabbitMQ
{
    public interface IRabbitMQService
    {
        public void SendMessage<T>(T message);
    }
}
