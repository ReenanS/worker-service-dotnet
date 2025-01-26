using Application.Data;
using Application.Products.Get;
using Application.Products.GetById;
using Domain.Entities;
using Domain.Products;
using Infrastructure.DataProviders.WebServices.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class JobD : BackgroundService
{
    private readonly ILogger<JobD> _logger;
    private readonly IConfiguration _configuration;
    private readonly PokemonService _pokemonService;
    private readonly IMediator _mediator;
    private readonly IServiceScopeFactory _serviceScopeFactory;  // Adicionar para criar escopos

    public JobD(ILogger<JobD> logger, IConfiguration configuration, PokemonService pokemonService, IMediator mediator, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _pokemonService = pokemonService;
        _mediator = mediator;
        _serviceScopeFactory = serviceScopeFactory;  // Injeção do IServiceScopeFactory
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
        var pokemonNames = new[] { "pikachu", "charizard" };

        // Lista de tarefas para execução paralela
        var tasks = new List<Task<PokemonResponse>>();

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

                // Adiciona a tarefa de chamada da API à lista de tarefas
                var task = _pokemonService.GetPokemonInfoAsync(name);
                tasks.Add(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar tarefa para Pokémon {Name}", name);
            }
        }

        // Aguarda todas as tarefas concluírem
        var pokemonResponses = await Task.WhenAll(tasks);

        // Agora que todas as respostas foram obtidas, processamos os dados
        var pokemonAbilities = new Dictionary<string, List<string>>();

        foreach (var pokemonInfo in pokemonResponses)
        {
            LogPokemonDetails(pokemonInfo);

            // Obtém as habilidades do Pokémon atual
            var currentAbilities = pokemonInfo.Abilities.Select(a => a.Ability.Name).ToList();
            pokemonAbilities[pokemonInfo.Name] = currentAbilities;
        }

        // Comparar habilidades entre os Pokémon
        await CompareAbilitiesAsync(pokemonAbilities);

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

        // Logando as habilidades
        foreach (var ability in pokemon.Abilities)
        {
            _logger.LogInformation("Ability: {AbilityName}", ability.Ability.Name);
        }
    }

    // Comparar habilidades entre os Pokémon
    private async Task CompareAbilitiesAsync(Dictionary<string, List<string>> pokemonAbilities)
    {
        var abilitiesChanged = false;

        // Obter todas as habilidades dos Pokémon e comparar
        var abilityList = pokemonAbilities.Values.ToList();

        if (abilityList.Count > 1)
        {
            // Compara a lista de habilidades entre os Pokémon
            for (int i = 0; i < abilityList.Count; i++)
            {
                for (int j = i + 1; j < abilityList.Count; j++)
                {
                    var firstAbilities = abilityList[i];
                    var secondAbilities = abilityList[j];

                    // Verifica se as habilidades são diferentes
                    if (!firstAbilities.SequenceEqual(secondAbilities))
                    {
                        abilitiesChanged = true;
                        _logger.LogInformation("As habilidades dos Pokémon {Pokemon1} e {Pokemon2} são diferentes.", pokemonAbilities.Keys.ElementAt(i), pokemonAbilities.Keys.ElementAt(j));
                    }
                    else
                    {
                        _logger.LogInformation("As habilidades dos Pokémon {Pokemon1} e {Pokemon2} são iguais.", pokemonAbilities.Keys.ElementAt(i), pokemonAbilities.Keys.ElementAt(j));
                    }
                }
            }
        }

        // Se houve mudança nas habilidades, realiza uma ação (por exemplo, um delay)
        if (abilitiesChanged)
        {
            _logger.LogInformation("Habilidades foram alteradas entre os Pokémon. Realizando ação de delay.");

            // Aqui é onde a chamada ao MediatR é feita, garantindo que o DbContext seja resolvido
            using (var scope = _serviceScopeFactory.CreateScope())  // Criando um escopo
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Executa a consulta GetProductsQuery via MediatR

                Guid id = Guid.Parse("29ab3332-82ec-4b15-ad0d-3d502585f629");
                var query = new GetProductQuery(new ProductId(id)); // ou qualquer filtro desejado
                var products = await mediator.Send(query);

                //if (products.Any())
                //{
                //    _logger.LogInformation("Produtos encontrados: {ProductCount}", products.Count());
                //    // Aqui você pode aplicar a lógica de manipulação de dados que precisar
                //}
                //else
                //{
                //    _logger.LogWarning("Nenhum produto encontrado.");
                //}

                _logger.LogInformation("Produtos encontrados: {ProductCount}", products);

                var teste = "tesste";
                // Aqui você pode utilizar o dbContext para fazer mais operações se necessário
            }

            await Task.Delay(TimeSpan.FromSeconds(5)); // Delay caso haja alteração
        }
        else
        {
            _logger.LogInformation("Nenhuma alteração nas habilidades dos Pokémon.");
        }
    }

}
