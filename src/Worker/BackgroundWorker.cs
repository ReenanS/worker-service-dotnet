namespace Worker
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly ILogger<BackgroundWorker> _logger;

        public BackgroundWorker(ILogger<BackgroundWorker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado às {time}", DateTime.Now);

            // O Quartz.NET é responsável por executar os jobs reais
            return Task.CompletedTask;
        }
    }
}
