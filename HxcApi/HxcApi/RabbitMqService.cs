using System.Text;
using System.Text.Json;
using HxcApi.Common.Events.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HxcApi;
public class RabbitMqService : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqService(string queueName, IServiceProvider serviceProvider)
    {
        var factory = new ConnectionFactory() { HostName = "host.docker.internal", Port = 5672, UserName = "guest", Password = "guest" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _queueName = queueName;
        _serviceProvider = serviceProvider;

        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public void Send(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        Console.WriteLine($" [x] Sent '{message}'");
    }

    public RabbitMqService Receive<T>() where T : Event
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var eventInstance = DeserializeEvent<T>(message);

            await InvokeEventHandler(eventInstance, scope);

            Console.WriteLine($" [x] Received '{message}'");
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return this;
    }

    private T DeserializeEvent<T>(string message)
    {
        var sourceGenOptions = new JsonSerializerOptions
        {
            TypeInfoResolver = new AppJsonSerializerContext()
        };

        return (T)JsonSerializer.Deserialize(message, typeof(T), sourceGenOptions);
    }

    private async Task InvokeEventHandler<T>(T eventInstance, IServiceScope scope) where T : Event
    {
        var eventHandler = scope.ServiceProvider.GetService<IEventHandler<T>>();
        if (eventHandler != null)
        {
            await eventHandler.HandleAsync(eventInstance);
        }
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
