namespace Amboosh_Pokemon_Review_Service.Model;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    //Navigation Properties
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();
}