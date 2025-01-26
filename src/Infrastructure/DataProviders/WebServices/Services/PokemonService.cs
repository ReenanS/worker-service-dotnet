using Domain.Entities;
using Infrastructure.DataProviders.WebServices.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DataProviders.WebServices.Services;
public class PokemonService
{
    private readonly IPokeApi _pokeApi;
    private readonly ILogger<PokemonService> _logger;

    public PokemonService(IPokeApi pokeApi, ILogger<PokemonService> logger)
    {
        _pokeApi = pokeApi;
        _logger = logger;
    }

    public async Task<PokemonResponse> GetPokemonInfoAsync(string name)
    {
        try
        {
            var response = await _pokeApi.GetPokemonByNameAsync(name);
            _logger.LogInformation("Successfully retrieved data for {PokemonName}", name);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving data for {PokemonName}", name);
            throw;
        }
    }
}
