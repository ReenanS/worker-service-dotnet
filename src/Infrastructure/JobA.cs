using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure;

[DisallowConcurrentExecution]
public class JobA : IJob
{
    private readonly ILogger<JobA> _logger;

    public JobA(ILogger<JobA> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job A iniciado às {time}", DateTime.Now);

        return Task.CompletedTask;
    }
}