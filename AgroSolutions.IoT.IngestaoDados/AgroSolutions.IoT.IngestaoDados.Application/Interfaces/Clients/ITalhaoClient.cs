using AgroSolutions.IoT.IngestaoDados.Application.DTOs;

namespace AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Clients;

public interface ITalhaoClient
{
    Task<TalhaoDto?> ObterPorIdAsync(string talhaoId);
}
