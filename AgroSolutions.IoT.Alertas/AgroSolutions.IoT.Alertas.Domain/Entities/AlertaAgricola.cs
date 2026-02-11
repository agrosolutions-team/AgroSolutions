using AgroSolutions.IoT.Alertas.Domain.Enums;

namespace AgroSolutions.IoT.Alertas.Domain.Entities;

public class AlertaAgricola
{
    public Guid Id { get; } = Guid.NewGuid();
    public TipoAlerta Tipo { get; }
    public SeveridadeAlerta Severidade { get; }
    public string TalhaoId { get; }
    public string PropriedadeId { get; }
    public DateTime Timestamp { get; }
    public string Descricao { get; }

    public AlertaAgricola(
        TipoAlerta tipo,
        SeveridadeAlerta severidade,
        string descricao,
        string talhaoId,
        string propriedadeId,
        DateTime timestamp)
    {
        Tipo = tipo;
        Severidade = severidade;
        Descricao = descricao;
        TalhaoId = talhaoId;
        PropriedadeId = propriedadeId;
        Timestamp = timestamp;
    }
}