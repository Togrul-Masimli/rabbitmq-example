using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace WebAPI.MessageBrokers.RabbitMQ.Producers;

public abstract class ProducerBase<T> : RabbitClientBase, IRabbitMqProducer<T>
{
    protected abstract string ExchangeName { get; }
    protected abstract string RoutingKeyName { get; }
    protected abstract string AppId { get; }

    
    public void Publish(T @event)
    {
        try
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            var properties = Channel.CreateBasicProperties();
            properties.AppId = AppId;
            properties.ContentType = "application/json";
            properties.DeliveryMode = 1;
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            Channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKeyName, body: body, basicProperties: properties);
        }
        catch (Exception e)
        {
            throw new Exception("Unexpected error happened while publishing the email");
        }
    }
}