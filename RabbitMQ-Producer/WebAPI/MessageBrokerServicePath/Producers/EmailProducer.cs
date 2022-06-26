using WebAPI.MessageBrokers.RabbitMQ.Events;
using WebAPI.MessageBrokers.RabbitMQ.Producers;

namespace WebAPI.MessageBrokerServicePath.Producers;

public class EmailProducer : ProducerBase<EmailIntegrationEvent>, IProducer
{
    protected override string ExchangeName => "EmailExchange";
    protected override string RoutingKeyName => "email.address";
    protected override string AppId => "EmailProducer";
}