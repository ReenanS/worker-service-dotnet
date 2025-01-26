using Application;
using Application.Abstractions;
using Infrastructure;
using Persistence;
using Worker.Extensions;
using Worker.Services;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    // Registra a camada de infraestrutura, que inclui o Quartz e outros servi�os
    services.AddInfrastructure();

    services.AddPersistence(context.Configuration);
    services.AddApplication();

    // Supondo que voc� tenha a URL base configurada
    var baseUrl = "https://example.com";

    services.AddSingleton<ILinkService>(new LinkService(baseUrl));

});

var app = builder.Build();

// Verifica se o ambiente � de desenvolvimento e aplica as migra��es
using (var scope = app.Services.CreateScope())
{
    var environment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
    if (environment.IsDevelopment())
    {
        // Chama o m�todo de extens�o para aplicar as migra��es
        app.ApplyMigrations();
    }
}

app.Run();
