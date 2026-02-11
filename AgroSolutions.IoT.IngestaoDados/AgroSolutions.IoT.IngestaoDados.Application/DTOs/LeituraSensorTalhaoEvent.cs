using AgroSolutions.IoT.IngestaoDados.Domain.Entities;

namespace AgroSolutions.IoT.IngestaoDados.Application.DTOs;

public sealed class LeituraSensorTalhaoEvent
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

    public static LeituraSensorTalhaoEvent CreateFromEntity(LeituraSensorTalhao leituraSensorTalhao)
    {
        return new LeituraSensorTalhaoEvent
        {
            Id = leituraSensorTalhao.Id,
            SensorId = leituraSensorTalhao.SensorId,
            TalhaoId = leituraSensorTalhao.TalhaoId,
            PropriedadeId = leituraSensorTalhao.PropriedadeId,
            NomeTalhao = leituraSensorTalhao.NomeTalhao,
            CulturaPlantada = leituraSensorTalhao.CulturaPlantada,
            AreaEmHectares = leituraSensorTalhao.AreaEmHectares,
            Timestamp = leituraSensorTalhao.Timestamp,
            UmidadeSoloPercentual = leituraSensorTalhao.UmidadeSoloPercentual,
            TemperaturaCelsius = leituraSensorTalhao.TemperaturaCelsius,
            PrecipitacaoMm = leituraSensorTalhao.PrecipitacaoMm,
            UmidadeArPercentual = leituraSensorTalhao.UmidadeArPercentual,
            VelocidadeVentoKmh = leituraSensorTalhao.VelocidadeVentoKmh,
            CriadoEm = leituraSensorTalhao.CriadoEm
        };
    }
}