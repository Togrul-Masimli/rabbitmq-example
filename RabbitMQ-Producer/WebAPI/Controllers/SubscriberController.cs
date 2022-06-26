using Microsoft.AspNetCore.Mvc;
using WebAPI.MessageBrokers.RabbitMQ.Events;
using WebAPI.MessageBrokerServicePath.Producers;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class SubscriberController : ControllerBase
{
    private readonly IProducer _producer;

    public SubscriberController(IProducer producer)
    {
        _producer = producer;
    }

    [HttpPost]
    public IActionResult Subscribe([FromBody] Email email)
    {
        _producer.Publish(new EmailIntegrationEvent {EmailAddress = email.EmailAddress});
        return Ok();
    }
}