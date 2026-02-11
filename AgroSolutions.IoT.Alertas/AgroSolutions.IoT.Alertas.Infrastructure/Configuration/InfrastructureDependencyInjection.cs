using AgroSolutions.IoT.Alertas.Application.Interfaces.Observability;
using AgroSolutions.IoT.Alertas.Infrastructure.Observability;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroSolutions.IoT.Alertas.Infrastructure.Configuration;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventoObservabilidadePublisher, EventoObservabilidadePublisher>();
    }
}