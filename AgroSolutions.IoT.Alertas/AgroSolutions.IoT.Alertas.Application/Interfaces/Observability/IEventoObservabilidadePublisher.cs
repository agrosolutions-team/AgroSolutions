using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Domain.Entities;

namespace AgroSolutions.IoT.Alertas.Application.Interfaces.Observability;

public interface IEventoObservabilidadePublisher
{
    void PublicarLeituraProcessada(LeituraSensorTalhaoDto leitura);
    void PublicarAlerta(AlertaAgricola alerta);
}