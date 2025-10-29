using Microsoft.Extensions.Caching.Memory;
using pokemon_backend.Models;
using pokemon_backend.Response;
namespace pokemon_backend.Services
{
    public class PokemonApiService : IPokemonApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<PokemonApiService> _logger;

        public PokemonApiService(HttpClient httpClient, IMemoryCache memoryCache, ILogger<PokemonApiService> logger)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<(IEnumerable<PokemonDto> Pokemons, int Count, string? Next, string? Previous)> GetPokemonsAsync(int page, int pageSize, string? search)
        {
            var cacheKey = $"pokemons_list_{search ?? "all"}";

            if (!_memoryCache.TryGetValue(cacheKey, out PokemonApiResponse? cachedResponse))
            {
                _logger.LogInformation("Buscando en la pokeapi");
                var absoluteUri = new Uri("https://pokeapi.co/api/v2/pokemon?limit=1000");
                var response = await _httpClient.GetFromJsonAsync<PokemonApiResponse>(absoluteUri);
                cachedResponse = response;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTimeOffset.Now.Add(TimeSpan.FromMinutes(10))); //  el cache expira en la hora actual +10 minutos

                _memoryCache.Set(cacheKey, cachedResponse, cacheEntryOptions);
            }
            else
            {
                _logger.LogInformation("Buscando en caché");
            }

            if (cachedResponse == null) return (Enumerable.Empty<PokemonDto>(), 0, null, null);

            var allPokemons = cachedResponse.Results;

            if (!string.IsNullOrWhiteSpace(search))
            {
                allPokemons = allPokemons.Where(p => p.Name.Contains(search.ToLower())).ToList();
            }

            var totalCount = allPokemons.Count;
            var pagedPokemons = allPokemons
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PokemonDto { Name = p.Name, Url = p.Url })
                .ToList();

            string? nextUrl = (page * pageSize < totalCount) ? $"/api/pokemon?page={page + 1}&pageSize={pageSize}&search={search}" : null;
            string? prevUrl = (page > 1) ? $"/api/pokemon?page={page - 1}&pageSize={pageSize}&search={search}" : null;

            return (pagedPokemons, totalCount, nextUrl, prevUrl);
        }
    }
    }
