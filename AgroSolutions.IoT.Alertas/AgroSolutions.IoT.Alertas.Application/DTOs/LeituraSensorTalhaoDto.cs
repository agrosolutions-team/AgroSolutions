namespace AgroSolutions.IoT.Alertas.Application.DTOs;

public class LeituraSensorTalhaoDto
{
    public Guid Id { get; init; }
    public string SensorId { get; init; }
    public string TalhaoId { get; init; }
    public string PropriedadeId { get; init; }
    public string NomeTalhao { get; init; }
    public string CulturaPlantada { get; init; }
    public decimal AreaEmHectares { get; init; }
    public DateTime Timestamp { get; init; }
    public decimal UmidadeSoloPercentual { get; init; }
    public decimal TemperaturaCelsius { get; init; }
    public decimal PrecipitacaoMm { get; init; }
    public decimal UmidadeArPercentual { get; init; }
    public decimal VelocidadeVentoKmh { get; init; }
    public DateTime CriadoEm { get; init; }
}
