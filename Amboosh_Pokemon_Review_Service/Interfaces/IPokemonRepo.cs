using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Interfaces;

public interface IPokemonRepo
{
    ICollection<Pokemon> GetPokemons(int? pageNumber);
    Pokemon GetPokemon(int id);
    Pokemon GetPokemonByName(string pokeName);
    decimal GetPokemonRating(int pokeId);
    bool PokemonExists(int pokeId);
    bool PokemonExistsByName(string pokeName);

}