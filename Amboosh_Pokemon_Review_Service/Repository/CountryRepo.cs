using System.Diagnostics.CodeAnalysis;
using Amboosh_Library.Data.Paging;
using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;
using AutoMapper;

namespace Amboosh_Pokemon_Review_Service.Repository;

public class CountryRepo : ICountryRepo
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CountryRepo(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ICollection<Country> GetCountries()
    {
        var country =  _context.Countries.OrderBy(c => c.Id).ToList();
        return country;
    }

    public Country GetCountry(int countryId)
    {
        var country = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        return country;
    }

    public Country GetCountryByOwner(int ownerId)
    {
        var country = _context.Owners
            .Where(o => o.Id == ownerId)
            .Select(c => c.Country).FirstOrDefault();
        return country;
    }

    public ICollection<Owner> GetOwnersFromCountry(int countryId)
    {
        var country = _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        return country;
    }

    public bool CountryExists(int Id)
    {
        var country = _context.Countries.Any(c => c.Id == Id);
        return country;
    }
}