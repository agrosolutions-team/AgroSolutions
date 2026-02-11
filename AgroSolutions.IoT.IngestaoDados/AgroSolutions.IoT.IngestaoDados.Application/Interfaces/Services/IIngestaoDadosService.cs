using AgroSolutions.IoT.IngestaoDados.Application.DTOs;

namespace AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Services;

public interface IIngestaoDadosService
{
    Task<BaseResponseDto> ProcessarDadosAsync(IngestaoDadosRequestDto request);
}
