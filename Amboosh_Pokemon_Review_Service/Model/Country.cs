namespace Amboosh_Pokemon_Review_Service.Model;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    //Navigation Properties
    public ICollection<Owner> Owners { get; set; }
}