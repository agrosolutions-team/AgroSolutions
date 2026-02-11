using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Clients;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Messaging;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Repositories;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Data;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.HttpClients;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Messaging;
using AgroSolutions.IoT.IngestaoDados.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Configuration;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ILeituraSensorTalhaoRepository, LeituraSensorTalhaoRepository>();
        
        services.AddHttpClient<ITalhaoClient, TalhaoHttpClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("UriApiPropriedades") ?? "https://localhost:7001");
        });
        
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
    }
}
