namespace Amboosh_Pokemon_Review_Service.Model;

public class Reviewer
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //Navigation Properties
    public ICollection<Review> Reviews { get; set; }
}