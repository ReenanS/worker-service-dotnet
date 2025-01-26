using Infrastructure;
using Worker;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    // Registra a camada de infraestrutura, que inclui o Quartz e outros serviços
    services.AddInfrastructure();

    // Adiciona o Worker Service como um serviço de background
    services.AddHostedService<BackgroundWorker>();
});

var app = builder.Build();

await app.RunAsync();
