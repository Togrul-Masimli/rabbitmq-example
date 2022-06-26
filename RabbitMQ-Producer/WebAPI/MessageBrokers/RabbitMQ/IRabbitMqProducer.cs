namespace WebAPI.MessageBrokers.RabbitMQ;

public interface IRabbitMqProducer<in T>
{
    void Publish(T @event);
}