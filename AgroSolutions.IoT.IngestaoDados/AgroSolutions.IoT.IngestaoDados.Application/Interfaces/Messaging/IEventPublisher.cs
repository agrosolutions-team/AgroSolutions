namespace AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<T>(T message, string routingKey);
}