using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Domain.Entities;

namespace AgroSolutions.IoT.Alertas.Application.Interfaces.Regras;

public interface IRegraAlerta
{
    AlertaAgricola? Avaliar(LeituraSensorTalhaoDto leitura);
}