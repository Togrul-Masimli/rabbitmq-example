using System.Text;
using ConsoleConsumer.MessageBrokers.RabbitMQ;
using ConsoleConsumer.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace ConsoleConsumer;

public class ConsoleConsumer : RabbitClientBase, IHostedService
{
    public ConsoleConsumer()
    {
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += (sender, e) =>
        {
            var body = Encoding.UTF8.GetString(e.Body.ToArray());
            var subscriber = JsonConvert.DeserializeObject<Subscriber>(body);

            Console.WriteLine(subscriber.EmailAddress);
        };
        Channel.BasicConsume(queue: EmailQueue, autoAck: true, consumer: consumer, consumerTag: "", noLocal: false, exclusive: false, arguments: null);
        Console.WriteLine("Consumer Started");
        Console.ReadLine();
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Dispose();
        return Task.CompletedTask;
    }
}