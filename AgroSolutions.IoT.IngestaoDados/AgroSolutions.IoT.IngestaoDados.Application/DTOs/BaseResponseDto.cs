namespace AgroSolutions.IoT.IngestaoDados.Application.DTOs;

public class BaseResponseDto
{
    public int StatusCode { get; init; }
    public string Message { get; init; } = string.Empty;
    public string? CorrelationId { get; init; }
}
