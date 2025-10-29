using pokemon_backend.Models;

namespace pokemon_backend.Services
{
    public interface IPokemonApiService
    {
        Task<(IEnumerable<PokemonDto> Pokemons, int Count, string? Next, string? Previous)> GetPokemonsAsync(int page, int pageSize, string? search);
    }
}
