using Application.Abstractions;

namespace Worker.Services;

internal sealed class LinkService : ILinkService
{
    private readonly string _baseUrl;

    public LinkService(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public Link Generate(string endpointName, object? routeValues, string rel, string method)
    {
        // Aqui, você pode gerar a URL com base em uma URL base configurada
        var url = $"{_baseUrl}/{endpointName}";

        // Se routeValues for necessário, você pode adicionar como query string ou usar conforme necessário
        if (routeValues != null)
        {
            // Exemplo de como adicionar valores de rota, você pode ajustar conforme necessário
            url = $"{url}?{string.Join("&", routeValues.GetType().GetProperties().Select(p => $"{p.Name}={p.GetValue(routeValues)}"))}";
        }

        return new Link(url, rel, method);
    }
}
