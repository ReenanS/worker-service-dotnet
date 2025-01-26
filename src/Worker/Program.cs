using Infrastructure;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    // Registra a camada de infraestrutura, que inclui o Quartz e outros serviços
    services.AddInfrastructure();
});

var app = builder.Build();

app.Run();
