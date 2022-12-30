namespace Amboosh_Pokemon_Review_Service.Model;

public class PokemonOwner
{
    public int PokemonId { get; set; }
    public int OwnerId { get; set; }
    //Navigation Properties
    public Pokemon Pokemon { get; set; }
    public Owner Owner { get; set; }
}