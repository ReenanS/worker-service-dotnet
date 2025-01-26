using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure;
public class JobD : BackgroundService
{
    private readonly ILogger<JobD> _logger;
    private readonly IConfiguration _configuration;

    public JobD(ILogger<JobD> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
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
        // Sua lógica de negócios
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Simulação do processamento
        _logger.LogInformation("Job D processado com sucesso às {time}", DateTime.Now);
    }
}
