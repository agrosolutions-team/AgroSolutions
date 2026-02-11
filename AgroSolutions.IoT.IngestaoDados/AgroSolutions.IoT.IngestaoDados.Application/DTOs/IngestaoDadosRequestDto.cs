using System.ComponentModel.DataAnnotations;

namespace AgroSolutions.IoT.IngestaoDados.Application.DTOs;

public sealed class IngestaoDadosRequestDto
{
    [Required]
    public string SensorId { get; init; }
    [Required]
    public string TalhaoId { get; init; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    [Required]
    public LeituraSensorDto Leitura { get; init; }
}

public sealed class LeituraSensorDto
{
    /// <summary>
    /// Umidade do solo em percentual (%)
    /// </summary>
    [Range(0, 100)]
    public decimal UmidadeSoloPercentual { get; init; }

    /// <summary>
    /// Temperatura ambiente em graus Celsius (°C)
    /// </summary>
    [Range(-50, 80)]
    public decimal TemperaturaCelsius { get; init; }

    /// <summary>
    /// Precipitação acumulada em milímetros (mm)
    /// </summary>
    [Range(0, 1000)]
    public decimal PrecipitacaoMm { get; init; }

    /// <summary>
    /// Umidade do ar em percentual (%)
    /// </summary>
    [Range(0, 100)]
    public decimal UmidadeArPercentual { get; init; }

    /// <summary>
    /// Velocidade do vento em quilômetros por hora (km/h)
    /// </summary>
    [Range(0, 300)]
    public decimal VelocidadeVentoKmh { get; init; }
}
