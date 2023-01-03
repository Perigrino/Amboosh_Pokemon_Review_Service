using Amboosh_Library.Data.Paging;
using Amboosh_Pokemon_Review_Service.Data;
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

    public ICollection<Pokemon> GetPokemons(int? pageNumber)
    {
        var pokemon = _context.Pokemons.OrderBy(p=>p.Id).ToList();
        
        //Paging
        int pageSize = 10;
        pokemon = PaginatedList<Pokemon>.Create(pokemon.AsQueryable(), pageNumber ?? 1, pageSize);
        return pokemon;
    }

    public Pokemon GetPokemon(int id)
    {
        var pokemon = _context.Pokemons.Where(p=> p.Id == id).FirstOrDefault();
        return pokemon;
    }

    public Pokemon GetPokemon(string name)
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
}