using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Observability;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Regras;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace AgroSolutions.IoT.Alertas.Application.Services;

public class AnaliseLeituraSensorService : IAnaliseLeituraSensorService
{
    private readonly IEventoObservabilidadePublisher _observabilidadePublisher;
    private readonly IEnumerable<IRegraAlerta> _regras;
    private readonly ILogger<AnaliseLeituraSensorService> _logger;

    public AnaliseLeituraSensorService(
        IEventoObservabilidadePublisher observabilidadePublisher,
        IEnumerable<IRegraAlerta> regras,
        ILogger<AnaliseLeituraSensorService> logger)
    {
        _observabilidadePublisher = observabilidadePublisher;
        _regras = regras;
        _logger = logger;
    }

    public Task AnalisarLeituraSensorAsync(LeituraSensorTalhaoDto leitura)
    {
        _observabilidadePublisher.PublicarLeituraProcessada(leitura);

        int alertaasEmitidos = 0;
        foreach (var regra in _regras)
        {
            var alerta = regra.Avaliar(leitura);

            if (alerta is null) continue;
            
            _observabilidadePublisher.PublicarAlerta(alerta);
            alertaasEmitidos++;
        }

        _logger.LogInformation("Leitura do sensor e {alertaasEmitidos} alerta(s) enviados como Custom Events do New Relic.",
            alertaasEmitidos);
        
        return Task.CompletedTask;
    }
}