using AgroSolutions.IoT.IngestaoDados.Domain.Entities;

namespace AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Repositories;

public interface ILeituraSensorTalhaoRepository
{
    Task AdicionarAsync(LeituraSensorTalhao leituraSensorTalhao);
}
