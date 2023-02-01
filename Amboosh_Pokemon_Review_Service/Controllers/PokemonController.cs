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
        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepo pokemonRepo, IMapper iMapper, IReviewRepo reviewRepo)
        {
            _pokemonRepo = pokemonRepo;
            _mapper = iMapper;
            _reviewRepo = reviewRepo;
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
        
        // POST: api/Pokemon
        [HttpPost]
        public IActionResult Post([FromQuery] int ownerId,[FromQuery] int categoryId, [FromBody] PokemonDto createPokemon)
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
            if (!_pokemonRepo.CreatePokemon(ownerId,categoryId,pokemonMap))
            {
                ModelState.AddModelError("","There was a problem whiles adding your Pokemon");
                return StatusCode(500, ModelState);
            }
            return Ok("Pokemon has been added successfully");
        }
        
        
        // PUT: api/Pokemon/5
        [HttpPut("{pokemonId}")]
        public IActionResult PutPokemon (int pokemonId,[FromQuery]int ownerId, [FromQuery]int categoryId, [FromBody] PokemonDto createPokemon)
        {
            if (createPokemon == null)
                return BadRequest(ModelState);

            if (pokemonId != createPokemon.Id)
                return BadRequest(ModelState);
            
            if (!_pokemonRepo.PokemonExists(pokemonId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(createPokemon);

            if (!_pokemonRepo.UpdatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("","Something went wrong whiles updating Pokemon details");
                return StatusCode(500, ModelState);
            }

            return Ok("Pokemon details have been updated successfully.");
        }
        
        // DELETE: api/Review/5
        [HttpDelete("{pokemonId}")]
        public IActionResult Delete(int pokemonId)
        {
            if (!_pokemonRepo.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepo.GetReviewOfPokemon(pokemonId);
            var pokemonToDelete = _pokemonRepo.GetPokemon(pokemonId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepo.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_pokemonRepo.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return Ok("Pokemon deleted successfully");
        }

        
    }
}