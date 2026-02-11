using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Messaging;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Configuration;

public static class MessagingConfiguration
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
        return services;
    }
}