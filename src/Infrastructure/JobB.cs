using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure;

[DisallowConcurrentExecution]
public class JobB : IJob
{
    private readonly ILogger<JobB> _logger;

    public JobB(ILogger<JobB> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job B iniciado via Quartz às {time}", DateTime.Now);
        // Lógica específica do JobB
        return Task.CompletedTask;
    }
}
