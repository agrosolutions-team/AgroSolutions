using AgroSolutions.IoT.Alertas.Application.DTOs;

namespace AgroSolutions.IoT.Alertas.Application.Interfaces.Services;

public interface IAnaliseLeituraSensorService
{
    Task AnalisarLeituraSensorAsync(LeituraSensorTalhaoDto leitura);
}
