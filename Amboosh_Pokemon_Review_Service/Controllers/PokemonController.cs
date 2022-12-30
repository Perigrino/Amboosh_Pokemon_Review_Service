using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepo _pokemonRepo;

        public PokemonController(IPokemonRepo pokemonRepo)
        {
            _pokemonRepo = pokemonRepo;
        }

        // GET: api/Pokemon
        [HttpGet]
        public  IActionResult GetPokemons()
        {
            var pokemon =  _pokemonRepo.GetPokemons();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }

        // GET: api/Pokemon/pokemonId
        [HttpGet("{pokemonId}")]
        public IActionResult GetPokemon(int pokemonId)
        {
            if (!_pokemonRepo.PokemonExists(pokemonId))
                return NotFound();
            
            var pokemon = _pokemonRepo.GetPokemon(pokemonId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(pokemon);
        }
        
        // GET: api/Pokemon/pokemonId/rating
        [HttpGet("{pokemonId}/rating")]
        public IActionResult GetPokemonRating(int pokemonId)
        {
            if (!_pokemonRepo.PokemonExists(pokemonId))
                return BadRequest();
            var pokemon = _pokemonRepo.GetPokemonRating(pokemonId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemon);

        }
        
    }
}