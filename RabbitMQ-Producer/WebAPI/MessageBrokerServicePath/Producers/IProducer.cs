using WebAPI.MessageBrokers.RabbitMQ.Events;

namespace WebAPI.MessageBrokerServicePath.Producers;

public interface IProducer
{
    void Publish(EmailIntegrationEvent @event);
}