using System.Text;
using System.Text.Json;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Messaging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Messaging;

public sealed class RabbitMqEventPublisher : IEventPublisher
{
    private readonly RabbitMqOptions _options;
    private readonly IConnection _connection;

    public RabbitMqEventPublisher(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;

        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        _connection = factory.CreateConnectionAsync().Result;
    }

    public async Task PublishAsync<T>(T message, string routingKey)
    {
        using var channel = await _connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            _options.Exchange,
            ExchangeType.Topic,
            true);

        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(message));

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(
            _options.Exchange,
            routingKey,
            false,
            properties,
            body);
        
        await channel.CloseAsync();
    }
}