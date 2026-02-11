using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Services;
using AgroSolutions.IoT.IngestaoDados.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgroSolutions.IoT.IngestaoDados.Application.Configuration;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IIngestaoDadosService, IngestaoDadosService>();

        return services;
    }
}
