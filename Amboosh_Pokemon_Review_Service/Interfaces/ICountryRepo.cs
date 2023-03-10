using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Interfaces;

public interface ICountryRepo
{
    ICollection<Country> GetCountries();
    Country GetCountry(int countryId);
    Country GetCountryByOwner(int ownerId);
    ICollection<Owner> GetOwnersFromCountry(int ownerId);
    bool CountryExists(int Id);
    bool CreateCountry(Country Country);
    bool UpdateCountry(Country country);
    bool DeleteCountry(Country country);
    bool Save();
}