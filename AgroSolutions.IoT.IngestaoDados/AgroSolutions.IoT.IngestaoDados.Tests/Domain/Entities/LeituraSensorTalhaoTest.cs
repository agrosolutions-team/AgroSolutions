using AgroSolutions.IoT.IngestaoDados.Domain.Entities;

namespace AgroSolutions.IoT.IngestaoDados.Tests.Domain.Entities;

public class LeituraSensorTalhaoTest
{
    [Fact]
    public void Constructor_DeveInicializarPropriedades_Corretamente()
    {
        // Arrange
        var sensorId = "sensor-001";
        var talhaoId = "talhao-01";
        var propriedadeId = "propriedade-99";
        var nomeTalhao = "Talhão Norte";
        var culturaPlantada = "Soja";
        var areaEmHectares = 12.34m;
        var timestamp = new DateTime(2026, 02, 22, 10, 30, 00, DateTimeKind.Utc);
        var umidadeSoloPercentual = 45.6m;
        var temperaturaCelsius = 28.9m;
        var precipitacaoMm = 3.2m;
        var umidadeArPercentual = 62.1m;
        var velocidadeVentoKmh = 11.7m;

        var antes = DateTime.UtcNow;

        // Act
        var leitura = new LeituraSensorTalhao(
            sensorId,
            talhaoId,
            propriedadeId,
            nomeTalhao,
            culturaPlantada,
            areaEmHectares,
            timestamp,
            umidadeSoloPercentual,
            temperaturaCelsius,
            precipitacaoMm,
            umidadeArPercentual,
            velocidadeVentoKmh);

        var depois = DateTime.UtcNow;

        // Assert
        Assert.NotEqual(Guid.Empty, leitura.Id);

        Assert.Equal(sensorId, leitura.SensorId);
        Assert.Equal(talhaoId, leitura.TalhaoId);
        Assert.Equal(propriedadeId, leitura.PropriedadeId);
        Assert.Equal(nomeTalhao, leitura.NomeTalhao);
        Assert.Equal(culturaPlantada, leitura.CulturaPlantada);
        Assert.Equal(areaEmHectares, leitura.AreaEmHectares);
        Assert.Equal(timestamp, leitura.Timestamp);
        Assert.Equal(umidadeSoloPercentual, leitura.UmidadeSoloPercentual);
        Assert.Equal(temperaturaCelsius, leitura.TemperaturaCelsius);
        Assert.Equal(precipitacaoMm, leitura.PrecipitacaoMm);
        Assert.Equal(umidadeArPercentual, leitura.UmidadeArPercentual);
        Assert.Equal(velocidadeVentoKmh, leitura.VelocidadeVentoKmh);
        Assert.True(leitura.CriadoEm >= antes && leitura.CriadoEm <= depois);
        Assert.Equal(DateTimeKind.Utc, leitura.CriadoEm.Kind);
    }
}