using Microsoft.AspNetCore.Mvc;
using pokemon_backend.Services;

namespace pokemon_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonApiService _pokeApiService;

        public PokemonController(IPokemonApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 20;

            var (pokemons, count, next, previous) = await _pokeApiService.GetPokemonsAsync(page, pageSize, search);

            var response = new
            {
                Data = pokemons,
                Pagination = new
                {
                    Count = count,
                    CurrentPage = page,
                    PageSize = pageSize,
                    NextUrl = next,
                    PreviousUrl = previous
                }
            };

            return Ok(response);
        }
    }
}
