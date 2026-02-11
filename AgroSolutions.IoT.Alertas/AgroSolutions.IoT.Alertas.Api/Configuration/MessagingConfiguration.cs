using AgroSolutions.IoT.Alertas.Api.BackgroundServices;
using AgroSolutions.IoT.Alertas.Api.Messaging;

namespace AgroSolutions.IoT.Alertas.Api.Configuration;

public static class MessagingConfiguration
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.AddHostedService<LeituraSensorConsumer>();
        return services;
    }
}