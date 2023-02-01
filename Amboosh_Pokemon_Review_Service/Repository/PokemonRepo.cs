using Amboosh_Library.Data.Paging;
using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Repository;

public class PokemonRepo : IPokemonRepo
{
    private readonly AppDbContext _context;
    public PokemonRepo(AppDbContext context)
    {
        _context = context;
    }

    public ICollection<Pokemon> GetPokemons()
    {
        var pokemon = _context.Pokemons.OrderBy(p=>p.Id).ToList();
        return pokemon;
    }

    public Pokemon GetPokemon(int id)
    {
        var pokemon = _context.Pokemons.Where(p=> p.Id == id).FirstOrDefault();
        return pokemon;
    }

    public Pokemon GetPokemonByName(string name)
    {
        var pokemon = _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        return pokemon;
    }

    public decimal GetPokemonRating(int pokeId)
    {
        var review = _context.Reviews.Where(r => r.Pokemon.Id == pokeId);
        if (review.Count() <= 0)
        {
            return 0;
        }
        return ((decimal)review.Sum(r => r.Rating) / review.Count());
    }
    

    public bool PokemonExists(int pokeId)
    {
        return _context.Pokemons.Any(p => p.Id == pokeId);
    }

    public bool PokemonExistsByName(string pokeName)
    {
        return _context.Pokemons.Any(p => p.Name == pokeName);
    }

    public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
    {
        var pokemonOwnerEntity = _context.Owners.Where(po => po.Id == ownerId).FirstOrDefault();
        var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

        var pokemonOwner = new PokemonOwner()
        {
            Owner = pokemonOwnerEntity,
            Pokemon = pokemon,
        };
        _context.Add(pokemonOwner);

        var pokemonCategory = new PokemonCategory()
        {
            Category = category,
            Pokemon = pokemon,
        };
        _context.Add(pokemonCategory);
        _context.Add(pokemon);
        return Save();
    }

    public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
    {
        _context.Update(pokemon);
        return Save();
    }

    public bool DeletePokemon(Pokemon pokemon)
    {
        _context.Remove(pokemon);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}