using Infrastructure.DataProviders.WebServices.Interfaces;
using Infrastructure.DataProviders.WebServices.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Refit;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {

        // Configuração do Quartz para Jobs agendados
        services.AddQuartz();

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        // Configuração adicional de Jobs
        services.ConfigureOptions<QuartzJobSetup>();

        // Registra o JobDBackgroundService como um serviço de background
        services.AddHostedService<JobD>();

        // Configuração do Refit para consumir a PokéAPI
        services.AddRefitClient<IPokeApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://pokeapi.co/api/v2/"));

        // Registra o serviço PokemonService
        services.AddSingleton<PokemonService>();

    }
}
