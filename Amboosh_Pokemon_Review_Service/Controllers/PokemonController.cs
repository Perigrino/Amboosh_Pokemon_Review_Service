using Microsoft.AspNetCore.Mvc;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;
using AutoMapper;

namespace Amboosh_Pokemon_Review_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepo _pokemonRepo;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepo pokemonRepo, IMapper iMapper)
        {
            _pokemonRepo = pokemonRepo;
            _mapper = iMapper;
        }

        // GET: api/Pokemon
        [HttpGet]
        public  IActionResult GetPokemons()
        {
            var pokemon =  _mapper.Map<List<PokemonDto>>(_pokemonRepo.GetPokemons());
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
            
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepo.GetPokemon(pokemonId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(pokemon);
        }
        
        // GET: api/Pokemon/pokemonName
        [HttpGet("{pokemonName}/pokemon_name")]
        public IActionResult GetPokemon(string pokemonName)
        {
            if (!_pokemonRepo.PokemonExistsByName(pokemonName))
                return NotFound();
            
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepo.GetPokemonByName(pokemonName));
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
        
        // POST: api/Owner
        [HttpPost]
        public IActionResult Post([FromQuery] int ownerId,int categoryId, [FromBody] PokemonDto createPokemon)
        {
            if (createPokemon == null)
                return BadRequest(ModelState);

            var pokemon = _pokemonRepo.GetPokemons()
                .Where(p => p.Name.Trim().ToUpper() == createPokemon.Name.Trim().ToUpper()).FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "This pokemon already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemonMap = _mapper.Map<Pokemon>(createPokemon);
            if (!_pokemonRepo.CreatePokemon(ownerId,categoryId,pokemon))
            {
                ModelState.AddModelError("","There was a problem whiles adding your Pokemon");
                return StatusCode(500, ModelState);
            }
            return Ok("Pokemon has been added successfully");
        }
        
    }
}