using System.Collections;
using RabbitMQ.Client;
using WebAPI.Configurations;

namespace WebAPI.MessageBrokers.RabbitMQ;

public abstract class RabbitClientBase : IDisposable
{
    protected readonly string EmailExchange = "EmailExchange";
    protected readonly string EmailQueue = "email.address";
    protected const string EmailQueueExchangeRoutingKey = "email.address";

    protected IModel Channel { get; private set; }
    private IConnection _connection;


    public RabbitClientBase()
    {
        ConnectToRabbitMq();
    }

    private void ConnectToRabbitMq()
    {
        var factory = new ConnectionFactory();
        _connection = factory.CreateConnection();

        if (Channel is null || !Channel.IsOpen)
        {
            Channel = _connection.CreateModel();

            Channel.ExchangeDeclare(
                exchange: EmailExchange,
                type: "direct",
                durable: true,
                autoDelete: false);
     
            Channel.QueueDeclare(
                queue: EmailQueue,
                durable: false,
                exclusive: false,
                autoDelete: false);

            Channel.QueueBind(
                queue: EmailQueue,
                exchange: EmailExchange,
                routingKey: EmailQueueExchangeRoutingKey);
        }
    }

    public void Dispose()
    {
        Channel?.Close();
        Channel?.Dispose();
        Channel = null;

        _connection?.Close();
        _connection?.Dispose();
        _connection = null;
    }
}