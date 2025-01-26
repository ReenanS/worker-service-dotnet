namespace Domain.Entities;
public class PokemonResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TypeInfo> Types { get; set; }
    public List<AbilityInfo> Abilities { get; set; } // Adicionada para representar as habilidades

}

public class TypeInfo
{
    public TypeDetails Type { get; set; }
}

public class TypeDetails
{
    public string Name { get; set; }
}

public class AbilityInfo
{
    public AbilityDetails Ability { get; set; }
}

public class AbilityDetails
{
    public string Name { get; set; }
}
