using AgroSolutions.IoT.IngestaoDados.Application.DTOs;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Clients;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Messaging;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Repositories;
using AgroSolutions.IoT.IngestaoDados.Application.Services;
using AgroSolutions.IoT.IngestaoDados.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace AgroSolutions.IoT.IngestaoDados.Tests.Applicaion.Services;

public class IngestaoDadosServiceTests
{
    [Fact]
    public async Task ProcessarDadosAsync_QuandoTalhaoNaoEncontrado_DeveRetornar404_ENaoPersistirNemPublicar()
    {
        // Arrange
        var talhaoClientMock = new Mock<ITalhaoClient>(MockBehavior.Strict);
        var repositoryMock = new Mock<ILeituraSensorTalhaoRepository>(MockBehavior.Strict);
        var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
        var loggerMock = new Mock<ILogger<IngestaoDadosService>>();

        var request = CriarRequest();

        talhaoClientMock
            .Setup(x => x.ObterPorIdAsync(request.TalhaoId))
            .ReturnsAsync((TalhaoDto?)null);

        var sut = new IngestaoDadosService(
            talhaoClientMock.Object,
            repositoryMock.Object,
            publisherMock.Object,
            loggerMock.Object);

        // Act
        var response = await sut.ProcessarDadosAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.StatusCode);
        Assert.Equal("Talhão não encontrado.", response.Message);

        talhaoClientMock.Verify(x => x.ObterPorIdAsync(request.TalhaoId), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
        publisherMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ProcessarDadosAsync_QuandoTalhaoEncontrado_DevePersistirPublicarEventoERetornar202()
    {
        // Arrange
        var talhaoClientMock = new Mock<ITalhaoClient>(MockBehavior.Strict);
        var repositoryMock = new Mock<ILeituraSensorTalhaoRepository>(MockBehavior.Strict);
        var publisherMock = new Mock<IEventPublisher>(MockBehavior.Strict);
        var loggerMock = new Mock<ILogger<IngestaoDadosService>>();

        var request = CriarRequest();

        var talhao = new TalhaoDto
        {
            Id = request.TalhaoId,
            PropriedadeId = Guid.NewGuid().ToString(),
            Nome = "Talhão A",
            CulturaPlantada = "Soja",
            AreaEmHectares = 12.5m
        };

        talhaoClientMock
            .Setup(x => x.ObterPorIdAsync(request.TalhaoId))
            .ReturnsAsync(talhao);

        repositoryMock
            .Setup(x => x.AdicionarAsync(It.IsAny<LeituraSensorTalhao>()))
            .Returns(Task.CompletedTask);

        publisherMock
            .Setup(x => x.PublishAsync(It.IsAny<LeituraSensorTalhaoEvent>(), "leitura.sensor.processada"))
            .Returns(Task.CompletedTask);

        var sut = new IngestaoDadosService(
            talhaoClientMock.Object,
            repositoryMock.Object,
            publisherMock.Object,
            loggerMock.Object);

        // Act
        var response = await sut.ProcessarDadosAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(202, response.StatusCode);
        Assert.Equal("Dados aceitos para processamento assíncrono.", response.Message);
        Assert.False(string.IsNullOrWhiteSpace(response.CorrelationId));

        talhaoClientMock.Verify(x => x.ObterPorIdAsync(request.TalhaoId), Times.Once);

        repositoryMock.Verify(x => x.AdicionarAsync(It.Is<LeituraSensorTalhao>(entity =>
            entity.SensorId == request.SensorId &&
            entity.TalhaoId == talhao.Id &&
            entity.PropriedadeId == talhao.PropriedadeId &&
            entity.NomeTalhao == talhao.Nome &&
            entity.CulturaPlantada == talhao.CulturaPlantada &&
            entity.AreaEmHectares == talhao.AreaEmHectares &&
            entity.Timestamp == request.Timestamp &&
            entity.UmidadeSoloPercentual == request.Leitura.UmidadeSoloPercentual &&
            entity.TemperaturaCelsius == request.Leitura.TemperaturaCelsius &&
            entity.PrecipitacaoMm == request.Leitura.PrecipitacaoMm &&
            entity.UmidadeArPercentual == request.Leitura.UmidadeArPercentual &&
            entity.VelocidadeVentoKmh == request.Leitura.VelocidadeVentoKmh
        )), Times.Once);

        publisherMock.Verify(x => x.PublishAsync(
            It.Is<LeituraSensorTalhaoEvent>(evt => evt != null),
            "leitura.sensor.processada"), Times.Once);

        repositoryMock.VerifyNoOtherCalls();
        publisherMock.VerifyNoOtherCalls();
    }

    private static IngestaoDadosRequestDto CriarRequest()
    {
        return new IngestaoDadosRequestDto
        {
            TalhaoId = Guid.NewGuid().ToString(),
            SensorId = Guid.NewGuid().ToString(),
            Leitura = new LeituraSensorDto
            {
                UmidadeSoloPercentual = 45.2m,
                TemperaturaCelsius = 28.1m,
                PrecipitacaoMm = 3.4m,
                UmidadeArPercentual = 62.8m,
                VelocidadeVentoKmh = 10.0m
            }
        };
    }
}