using AgroSolutions.IoT.IngestaoDados.Application.DTOs;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Clients;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Messaging;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Repositories;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Services;
using AgroSolutions.IoT.IngestaoDados.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace AgroSolutions.IoT.IngestaoDados.Application.Services;

public class IngestaoDadosService : IIngestaoDadosService
{
    private readonly ITalhaoClient _talhaoClient;
    private readonly ILeituraSensorTalhaoRepository _repository;
    private readonly IEventPublisher _publisher;
    private readonly ILogger<IngestaoDadosService> _logger;

    public IngestaoDadosService(
        ITalhaoClient talhaoClient,
        ILeituraSensorTalhaoRepository repository,
        IEventPublisher publisher,
        ILogger<IngestaoDadosService> logger)
    {
        _talhaoClient = talhaoClient;
        _repository = repository;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<BaseResponseDto> ProcessarDadosAsync(IngestaoDadosRequestDto request)
    {
        var talhao = await _talhaoClient.ObterPorIdAsync(request.TalhaoId);

        if (talhao is null)
        {
            return new BaseResponseDto
            {
                StatusCode = 404,
                Message = "Talhão não encontrado."
            };
        }
        
        // TODO para futuras versões:
        // Dados obtidos do talhão podem ser gravados em cache nessa etapa.
        // Então, o service pode sempre consultar primeiro em cache. Consulta na API somente em caso de não existir em cache.

        var leituraSensorTalhao = new LeituraSensorTalhao(
            request.SensorId,
            talhao.Id,
            talhao.PropriedadeId,
            talhao.Nome,
            talhao.CulturaPlantada,
            talhao.AreaEmHectares,
            request.Timestamp,
            request.Leitura.UmidadeSoloPercentual,
            request.Leitura.TemperaturaCelsius,
            request.Leitura.PrecipitacaoMm,
            request.Leitura.UmidadeArPercentual,
            request.Leitura.VelocidadeVentoKmh);
        
        await _repository.AdicionarAsync(leituraSensorTalhao);
        
        var evento = LeituraSensorTalhaoEvent.CreateFromEntity(leituraSensorTalhao);
        
        await _publisher.PublishAsync(
            evento,
            routingKey: "leitura.sensor.processada");
        
        return new BaseResponseDto
        {
            StatusCode = 202,
            Message = "Dados aceitos para processamento assíncrono.",
            CorrelationId = leituraSensorTalhao.Id.ToString()
        };
    }
}
