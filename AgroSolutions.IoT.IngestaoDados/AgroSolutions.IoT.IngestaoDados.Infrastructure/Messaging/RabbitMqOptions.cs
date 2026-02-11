namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Messaging;

public sealed class RabbitMqOptions
{
    public string HostName { get; init; }
    public int Port { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string Exchange { get; init; }
}