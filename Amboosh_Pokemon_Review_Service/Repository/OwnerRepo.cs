using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Repository;

public class OwnerRepo : IOwnerRepo
{
    private readonly AppDbContext _context;
    public OwnerRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public ICollection<Owner> GetOwners()
    {
        var owner = _context.Owners.OrderBy(o => o.Id).ToList();
        return owner;
    }

    public Owner GetOwner(int ownerId)
    {
        var owner = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        return owner;
    }

    public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId)
    {
        var owner = _context.PokemonOwners
            .Where(op => op.Pokemon.Id == pokemonId)
            .Select(o => o.Owner).ToList();
        return owner;
    }

    public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
    {
        var pokemon = _context.PokemonOwners
            .Where(po => po.Owner.Id == ownerId)
            .Select(p => p.Pokemon).ToList();
        return pokemon;
    }

    public bool OwnerExists(int ownerId)
    {
        return _context.Owners.Any(o => o.Id == ownerId);
    }

    public bool CreateOwner(Owner createOwner)
    {
        var owner = _context.Add(createOwner);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}