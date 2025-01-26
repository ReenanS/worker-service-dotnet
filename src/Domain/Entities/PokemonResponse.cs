namespace Domain.Entities
{
    public class PokemonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PokemonType[] Types { get; set; }
    }

    public class PokemonType
    {
        public TypeDetails Type { get; set; }
    }

    public class TypeDetails
    {
        public string Name { get; set; }
    }
}
