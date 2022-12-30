namespace Amboosh_Pokemon_Review_Service.Model;

public class Owner
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gym { get; set; }
    //Navigation Properties
    public Country Country { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
}