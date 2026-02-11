using System.Text;
using System.Text.Json;
using AgroSolutions.IoT.Alertas.Api.Messaging;
using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AgroSolutions.IoT.Alertas.Api.BackgroundServices;

public class LeituraSensorConsumer : BackgroundService
{
    private readonly RabbitMqOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<LeituraSensorConsumer> _logger;
    
    private IConnection _connection;
    private IChannel _channel;

    public LeituraSensorConsumer(
        IOptions<RabbitMqOptions> options,
        IServiceScopeFactory scopeFactory,
        ILogger<LeituraSensorConsumer> logger)
    {
        _options = options.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.ExchangeDeclareAsync(exchange: _options.Exchange, type: ExchangeType.Topic, true, cancellationToken: stoppingToken);
        
        QueueDeclareOk queueDeclareResult = await _channel.QueueDeclareAsync(cancellationToken: stoppingToken);
        string queueName = queueDeclareResult.QueueName;

        await _channel.QueueBindAsync(queue: queueName, exchange: _options.Exchange, routingKey: _options.RoutingKey, cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += OnMessageAsync;

        await _channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
        
        _logger.LogInformation("RabbitMQ Consumer da API de Alertas iniciado. Exchange: {Exchange}. Fila {QueueName}.",
            _options.Exchange,
            queueName);
    }

    private async Task OnMessageAsync(object model, BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        
        _logger.LogInformation(
            "Mensagem recebida na API de Alertas: {Message}. MessageId={MessageId}, CorrelationId={CorrelationId}",
            message, ea.BasicProperties.MessageId, ea.BasicProperties.CorrelationId);
        
        var leitura = JsonSerializer.Deserialize<LeituraSensorTalhaoDto>(message);
        if (leitura is not null)
        {
            using var scope = _scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IAnaliseLeituraSensorService>();
            
            await service.AnalisarLeituraSensorAsync(leitura);
        }
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel.CloseAsync(cancellationToken);
        await _channel.DisposeAsync();
        await _connection.CloseAsync(cancellationToken);
        await _connection.DisposeAsync();
        await base.StopAsync(cancellationToken);
    }
}
