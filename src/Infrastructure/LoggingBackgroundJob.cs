using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure;

[DisallowConcurrentExecution]
public class LoggingBackgroundJob : IJob
{
    private readonly ILogger<LoggingBackgroundJob> _logger;

    public LoggingBackgroundJob(ILogger<LoggingBackgroundJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job Tio Phon iniciado às {time}", DateTime.Now);

        return Task.CompletedTask;
    }
}