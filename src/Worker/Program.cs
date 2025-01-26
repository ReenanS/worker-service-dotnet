using Infrastructure;
using Worker;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    // Registra a camada de infraestrutura, que inclui o Quartz e outros servi�os
    services.AddInfrastructure();

    // Adiciona o Worker Service como um servi�o de background
    services.AddHostedService<BackgroundWorker>();
});

var app = builder.Build();

await app.RunAsync();
