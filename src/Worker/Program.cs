using Infrastructure;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    // Registra a camada de infraestrutura, que inclui o Quartz e outros serviços
    services.AddInfrastructure();

    // Adiciona o Worker Service como um serviço de background
    //services.AddHostedService<BackgroundWorker>();

    // Registra o JobDBackgroundService
    services.AddHostedService<JobD>();

});

var app = builder.Build();

app.Run();
