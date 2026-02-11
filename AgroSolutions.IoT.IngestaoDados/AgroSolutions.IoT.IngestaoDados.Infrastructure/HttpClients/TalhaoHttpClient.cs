using System.Net.Http.Json;
using AgroSolutions.IoT.IngestaoDados.Application.DTOs;
using AgroSolutions.IoT.IngestaoDados.Application.Interfaces.Clients;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.HttpClients;

public sealed class TalhaoHttpClient : ITalhaoClient
{
    private readonly HttpClient _httpClient;

    public TalhaoHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TalhaoDto?> ObterPorIdAsync(string talhaoId)
    {
        var response = await _httpClient.GetAsync($"/api/talhoes/{talhaoId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TalhaoDto>();
    }
}