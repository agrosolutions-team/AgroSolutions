namespace AgroSolutions.IoT.Alertas.Api.Messaging;

public class RabbitMqOptions
{
    public string HostName { get; init; }
    public int Port { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string Exchange { get; init; }
    public string RoutingKey { get; init; }
}