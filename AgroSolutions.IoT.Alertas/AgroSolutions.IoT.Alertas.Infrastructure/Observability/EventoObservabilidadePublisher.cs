using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Observability;
using AgroSolutions.IoT.Alertas.Domain.Entities;

namespace AgroSolutions.IoT.Alertas.Infrastructure.Observability;

public class EventoObservabilidadePublisher : IEventoObservabilidadePublisher
{
    public void PublicarLeituraProcessada(LeituraSensorTalhaoDto leitura)
    {
        var atributos = new Dictionary<string, object>
        {
            { "TalhaoId", leitura.TalhaoId },
            { "PropriedadeId", leitura.PropriedadeId },
            { "Cultura", leitura.CulturaPlantada },
            { "UmidadeSolo", leitura.UmidadeSoloPercentual },
            { "Temperatura", leitura.TemperaturaCelsius },
            { "Precipitacao", leitura.PrecipitacaoMm },
            { "UmidadeAr", leitura.UmidadeArPercentual },
            { "VelocidadeVento", leitura.VelocidadeVentoKmh }
        };
        
        NewRelic.Api.Agent.NewRelic.RecordCustomEvent(
            "LeituraSensorProcessada",
            atributos);
    }
    
    public void PublicarAlerta(AlertaAgricola alerta)
    {
        var atributos = new Dictionary<string, object>
        {
            { "TipoAlerta", alerta.Tipo.ToString() },
            { "Severidade", alerta.Severidade.ToString() },
            { "TalhaoId", alerta.TalhaoId },
            { "PropriedadeId", alerta.PropriedadeId },
            { "Descricao", alerta.Descricao }
        };
        
        NewRelic.Api.Agent.NewRelic.RecordCustomEvent(
            "AlertaAgricolaGerado",
            atributos);
    }
}
