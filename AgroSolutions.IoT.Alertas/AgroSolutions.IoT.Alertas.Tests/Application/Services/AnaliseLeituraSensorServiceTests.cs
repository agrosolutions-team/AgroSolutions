using AgroSolutions.IoT.Alertas.Application.DTOs;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Observability;
using AgroSolutions.IoT.Alertas.Application.Interfaces.Regras;
using AgroSolutions.IoT.Alertas.Application.Services;
using AgroSolutions.IoT.Alertas.Domain.Entities;
using AgroSolutions.IoT.Alertas.Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;

namespace AgroSolutions.IoT.Alertas.Tests.Application.Services;

public class AnaliseLeituraSensorServiceTests
{
    [Fact]
    public async Task AnalisarLeituraSensorAsync_DevePublicarLeituraProcessada_Sempre()
    {
        // Arrange
        var publisherMock = new Mock<IEventoObservabilidadePublisher>(MockBehavior.Strict);
        var regraMock = new Mock<IRegraAlerta>(MockBehavior.Strict);
        var loggerMock = new Mock<ILogger<AnaliseLeituraSensorService>>(MockBehavior.Loose);

        var leitura = CriarLeituraPadrao();

        publisherMock
            .Setup(p => p.PublicarLeituraProcessada(leitura));

        regraMock
            .Setup(r => r.Avaliar(leitura))
            .Returns((AlertaAgricola?)null);

        var sut = new AnaliseLeituraSensorService(
            publisherMock.Object,
            new List<IRegraAlerta> { regraMock.Object },
            loggerMock.Object);

        // Act
        await sut.AnalisarLeituraSensorAsync(leitura);

        // Assert
        publisherMock.Verify(p => p.PublicarLeituraProcessada(leitura), Times.Once);
        publisherMock.VerifyNoOtherCalls();
        regraMock.Verify(r => r.Avaliar(leitura), Times.Once);
    }

    [Fact]
    public async Task AnalisarLeituraSensorAsync_QuandoRegrasNaoGeramAlerta_NaoDevePublicarAlerta_EDeveLogarQuantidadeZero()
    {
        // Arrange
        var publisherMock = new Mock<IEventoObservabilidadePublisher>(MockBehavior.Strict);
        var regra1Mock = new Mock<IRegraAlerta>(MockBehavior.Strict);
        var regra2Mock = new Mock<IRegraAlerta>(MockBehavior.Strict);
        var loggerMock = new Mock<ILogger<AnaliseLeituraSensorService>>(MockBehavior.Loose);

        var leitura = CriarLeituraPadrao();

        publisherMock
            .Setup(p => p.PublicarLeituraProcessada(leitura));

        regra1Mock
            .Setup(r => r.Avaliar(leitura))
            .Returns((AlertaAgricola?)null);

        regra2Mock
            .Setup(r => r.Avaliar(leitura))
            .Returns((AlertaAgricola?)null);

        var sut = new AnaliseLeituraSensorService(
            publisherMock.Object,
            new List<IRegraAlerta> { regra1Mock.Object, regra2Mock.Object },
            loggerMock.Object);

        // Act
        await sut.AnalisarLeituraSensorAsync(leitura);

        // Assert
        publisherMock.Verify(p => p.PublicarLeituraProcessada(leitura), Times.Once);
        publisherMock.Verify(p => p.PublicarAlerta(It.IsAny<AlertaAgricola>()), Times.Never);
        regra1Mock.Verify(r => r.Avaliar(leitura), Times.Once);
        regra2Mock.Verify(r => r.Avaliar(leitura), Times.Once);
    }

    [Fact]
    public async Task AnalisarLeituraSensorAsync_QuandoAlgumasRegrasGeramAlerta_DevePublicarCadaAlerta_EDeveLogarQuantidade()
    {
        // Arrange
        var publisherMock = new Mock<IEventoObservabilidadePublisher>(MockBehavior.Strict);
        var regra1Mock = new Mock<IRegraAlerta>(MockBehavior.Strict);
        var regra2Mock = new Mock<IRegraAlerta>(MockBehavior.Strict);
        var regra3Mock = new Mock<IRegraAlerta>(MockBehavior.Strict);
        var loggerMock = new Mock<ILogger<AnaliseLeituraSensorService>>(MockBehavior.Loose);

        var leitura = CriarLeituraPadrao();

        var alerta1 = new AlertaAgricola(
            TipoAlerta.UmidadeSoloCritica,
            SeveridadeAlerta.Alta,
            "A1",
            leitura.TalhaoId,
            leitura.PropriedadeId,
            leitura.Timestamp);

        var alerta2 = new AlertaAgricola(
            TipoAlerta.TemperaturaElevada,
            SeveridadeAlerta.Media,
            "A2",
            leitura.TalhaoId,
            leitura.PropriedadeId,
            leitura.Timestamp);

        var sequence = new MockSequence();

        publisherMock.InSequence(sequence)
            .Setup(p => p.PublicarLeituraProcessada(leitura));

        regra1Mock.InSequence(sequence)
            .Setup(r => r.Avaliar(leitura))
            .Returns(alerta1);

        publisherMock.InSequence(sequence)
            .Setup(p => p.PublicarAlerta(alerta1));

        regra2Mock.InSequence(sequence)
            .Setup(r => r.Avaliar(leitura))
            .Returns((AlertaAgricola?)null);

        regra3Mock.InSequence(sequence)
            .Setup(r => r.Avaliar(leitura))
            .Returns(alerta2);

        publisherMock.InSequence(sequence)
            .Setup(p => p.PublicarAlerta(alerta2));

        var sut = new AnaliseLeituraSensorService(
            publisherMock.Object,
            new List<IRegraAlerta> { regra1Mock.Object, regra2Mock.Object, regra3Mock.Object },
            loggerMock.Object);

        // Act
        await sut.AnalisarLeituraSensorAsync(leitura);

        // Assert
        publisherMock.Verify(p => p.PublicarLeituraProcessada(leitura), Times.Once);
        publisherMock.Verify(p => p.PublicarAlerta(alerta1), Times.Once);
        publisherMock.Verify(p => p.PublicarAlerta(alerta2), Times.Once);
        publisherMock.VerifyNoOtherCalls();

        regra1Mock.Verify(r => r.Avaliar(leitura), Times.Once);
        regra2Mock.Verify(r => r.Avaliar(leitura), Times.Once);
        regra3Mock.Verify(r => r.Avaliar(leitura), Times.Once);
    }

    private static LeituraSensorTalhaoDto CriarLeituraPadrao()
    {
        return new LeituraSensorTalhaoDto
        {
            Id = Guid.NewGuid(),
            SensorId = "SENSOR-001",
            TalhaoId = "TALHAO-001",
            PropriedadeId = "PROP-001",
            NomeTalhao = "Talhão A",
            CulturaPlantada = "Soja",
            AreaEmHectares = 10m,
            Timestamp = new DateTime(2026, 01, 01, 12, 00, 00, DateTimeKind.Utc),
            UmidadeSoloPercentual = 15m,
            TemperaturaCelsius = 30m,
            PrecipitacaoMm = 0m,
            UmidadeArPercentual = 60m,
            VelocidadeVentoKmh = 10m,
            CriadoEm = new DateTime(2026, 01, 01, 12, 00, 00, DateTimeKind.Utc)
        };
    }
}