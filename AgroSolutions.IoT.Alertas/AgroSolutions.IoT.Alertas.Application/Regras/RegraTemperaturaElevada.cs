using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Regras;
using AgroSolutions.IoT.Alertas.Domain.Entities;
using AgroSolutions.IoT.Alertas.Domain.Enums;

namespace AgroSolutions.IoT.Alertas.Application.Regras;

public class RegraTemperaturaElevada : IRegraAlerta
{
    public AlertaAgricola? Avaliar(LeituraSensorTalhaoDto leitura)
    {
        if (leitura.TemperaturaCelsius <= 35)
            return null;

        return new AlertaAgricola(
            TipoAlerta.TemperaturaElevada,
            SeveridadeAlerta.Media,
            "Temperatura acima do limite recomendado",
            leitura.TalhaoId,
            leitura.PropriedadeId,
            leitura.Timestamp);
    }
}
