namespace pokemon_backend.Models
{
    public class PokemonDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        //id de url
        public int Id => int.Parse(Url.Split('/')[^2]);
    }
}
