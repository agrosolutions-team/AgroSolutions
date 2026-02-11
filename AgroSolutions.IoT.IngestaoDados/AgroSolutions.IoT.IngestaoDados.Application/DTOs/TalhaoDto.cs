namespace AgroSolutions.IoT.IngestaoDados.Application.DTOs;

public sealed class TalhaoDto
{
    public string Id { get; init; }
    public string PropriedadeId { get; init; }
    public string Nome { get; init; }
    public string CulturaPlantada { get; init; }
    public decimal AreaEmHectares { get; init; }
}
