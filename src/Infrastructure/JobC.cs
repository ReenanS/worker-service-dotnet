using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure;

[DisallowConcurrentExecution]
public class JobC : IJob
{
    private readonly ILogger<JobC> _logger;

    public JobC(ILogger<JobC> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job C iniciado via Quartz às {time}", DateTime.Now);
        // Lógica específica do JobC
        return Task.CompletedTask;
    }
}
