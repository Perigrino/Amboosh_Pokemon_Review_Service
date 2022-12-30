namespace Amboosh_Pokemon_Review_Service.Model;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    //Navigation Properties
    public ICollection<Review> Reviews { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
    public ICollection<PokemonCategory> PokemonCategories { get; set; }
}