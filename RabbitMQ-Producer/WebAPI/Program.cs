using RabbitMQ.Client;
using WebAPI.MessageBrokerServicePath.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProducer, EmailProducer>();
builder.Services.AddSingleton(serviceProvider =>
{
    var uri = new Uri("amqp://guest:guest@rabbit:5672");
    return new ConnectionFactory
    {
        Uri = uri
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();