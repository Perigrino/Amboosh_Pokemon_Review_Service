using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Dto;

public class OwnerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gym { get; set; }
    //Navigation Properties
    public Country Country { get; set; }
}