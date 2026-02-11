using AgroSolutions.IoT.Alertas.Application.Interfaces.Regras;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Services;
using AgroSolutions.IoT.Alertas.Application.Regras;
using AgroSolutions.IoT.Alertas.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgroSolutions.IoT.Alertas.Application.Configuration;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAnaliseLeituraSensorService, AnaliseLeituraSensorService>();
        services.AddScoped<IRegraAlerta, RegraUmidadeSoloCritica>();
        services.AddScoped<IRegraAlerta, RegraTemperaturaElevada>();
        return services;
    }
}