using Domain.Entities;
using Infrastructure.DataProviders.WebServices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class JobD : BackgroundService
{
    private readonly ILogger<JobD> _logger;
    private readonly IConfiguration _configuration;
    private readonly PokemonService _pokemonService;

    public JobD(ILogger<JobD> logger, IConfiguration configuration, PokemonService pokemonService)
    {
        _logger = logger;
        _configuration = configuration;
        _pokemonService = pokemonService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_configuration.GetValue<bool>("TRIGGER_JOB_D"))
        {
            _logger.LogInformation("JobD está desabilitado para execução.");
            return;
        }

        _logger.LogInformation("Job D iniciado via BackgroundService às {time}", DateTime.Now);

        // Lógica do Job
        await ProcessJobAsync(stoppingToken);
    }

    private async Task ProcessJobAsync(CancellationToken stoppingToken)
    {
        // Definimos os nomes dos Pokémon a serem consumidos.
        var pokemonNames = new[] { "pikachu", "mewtwo" };

        foreach (var name in pokemonNames)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Job D foi cancelado.");
                break;
            }

            try
            {
                _logger.LogInformation("Buscando informações do Pokémon {Name}", name);

                var pokemonInfo = await _pokemonService.GetPokemonInfoAsync(name);
                LogPokemonDetails(pokemonInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar informações do Pokémon {Name}", name);
            }
        }

        // Intervalo de 30 segundos entre execuções do job
        await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
    }

    private void LogPokemonDetails(PokemonResponse pokemon)
    {
        _logger.LogInformation("Pokemon ID: {Id}, Name: {Name}", pokemon.Id, pokemon.Name);
        foreach (var type in pokemon.Types)
        {
            _logger.LogInformation("Type: {TypeName}", type.Type.Name);
        }
    }

}
