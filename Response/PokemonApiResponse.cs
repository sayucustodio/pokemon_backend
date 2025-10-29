namespace pokemon_backend.Response
{
    public class PokemonApiResponse
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public List<PokemonResult> Results { get; set; } = new();
    }
    public class PokemonResult
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
