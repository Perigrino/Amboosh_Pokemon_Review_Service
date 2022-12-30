namespace Amboosh_Pokemon_Review_Service.Model;

public class PokemonCategory
{
    public int PokemonId { get; set; }
    public int CategoryId { get; set; }
    //Navigation Properties
    public Pokemon Pokemon { get; set; }
    public Category Category { get; set; }
}