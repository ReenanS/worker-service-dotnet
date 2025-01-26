using Domain.Entities;
using Refit;

namespace Infrastructure.DataProviders.WebServices.Interfaces;
public interface IPokeApi
{
    [Get("/pokemon/{name}")]
    Task<PokemonResponse> GetPokemonByNameAsync(string name);
}
