using AgroSolutions.IoT.Alertas.Domain.Entities;
using AgroSolutions.IoT.Alertas.Domain.Enums;

namespace AgroSolutions.IoT.Alertas.Tests.Domain.Entities;

public class AlertaAgricolaTests
{
    [Fact]
    public void Construtor_Deve_inicializar_todas_as_propriedades()
    {
        // Arrange
        var tipo = TipoAlerta.UmidadeSoloCritica;
        var severidade = SeveridadeAlerta.Alta;
        var descricao = "Umidade do solo crítica.";
        var talhaoId = "TALHAO-001";
        var propriedadeId = "PROP-001";
        var timestamp = new DateTime(2026, 02, 22, 10, 30, 00, DateTimeKind.Utc);

        // Act
        var alerta = new AlertaAgricola(
            tipo,
            severidade,
            descricao,
            talhaoId,
            propriedadeId,
            timestamp);

        // Assert
        Assert.NotEqual(Guid.Empty, alerta.Id);
        Assert.Equal(tipo, alerta.Tipo);
        Assert.Equal(severidade, alerta.Severidade);
        Assert.Equal(descricao, alerta.Descricao);
        Assert.Equal(talhaoId, alerta.TalhaoId);
        Assert.Equal(propriedadeId, alerta.PropriedadeId);
        Assert.Equal(timestamp, alerta.Timestamp);
    }

    [Fact]
    public void Construtor_Deve_gerar_Id_diferente_para_cada_instancia()
    {
        // Arrange
        var tipo = TipoAlerta.UmidadeSoloCritica;
        var severidade = SeveridadeAlerta.Alta;

        // Act
        var a1 = new AlertaAgricola(tipo, severidade, "Desc 1", "T1", "P1", DateTime.UtcNow);
        var a2 = new AlertaAgricola(tipo, severidade, "Desc 2", "T2", "P2", DateTime.UtcNow);

        // Assert
        Assert.NotEqual(a1.Id, a2.Id);
        Assert.NotEqual(Guid.Empty, a1.Id);
        Assert.NotEqual(Guid.Empty, a2.Id);
    }
}