using AgroSolutions.IoT.IngestaoDados.Application.DTOs;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroSolutions.IoT.IngestaoDados.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status500InternalServerError)]
public class IngestaoDadosController : ControllerBase
{
    private readonly IIngestaoDadosService _service;

    public IngestaoDadosController(IIngestaoDadosService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ProcessarDadosAsync([FromBody] IngestaoDadosRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Payload inválido.");

        var response = await _service.ProcessarDadosAsync(request);

        return StatusCode(response.StatusCode, response);
    }
}