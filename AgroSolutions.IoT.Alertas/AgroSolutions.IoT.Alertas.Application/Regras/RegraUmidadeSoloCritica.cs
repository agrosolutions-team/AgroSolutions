using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Regras;
using AgroSolutions.IoT.Alertas.Domain.Entities;
using AgroSolutions.IoT.Alertas.Domain.Enums;

namespace AgroSolutions.IoT.Alertas.Application.Regras;

public class RegraUmidadeSoloCritica : IRegraAlerta
{
    public AlertaAgricola? Avaliar(LeituraSensorTalhaoDto leitura)
    {
        if (leitura.UmidadeSoloPercentual >= 20)
            return null;

        return new AlertaAgricola(
            TipoAlerta.UmidadeSoloCritica,
            SeveridadeAlerta.Alta,
            "Umidade do solo abaixo do nível crítico",
            leitura.TalhaoId,
            leitura.PropriedadeId,
            leitura.Timestamp);
    }
}
