namespace estatedocflow.api.RabbitMQ
{
    public interface IRabbitMqService
    {
        public void SendMessage<T>(T message);
    }
}
