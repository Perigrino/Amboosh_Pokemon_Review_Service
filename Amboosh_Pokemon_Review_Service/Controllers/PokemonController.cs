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
        [HttpGet("pagenumber{pageNumber}")]
        public  IActionResult GetPokemons(int pageNumber)
        {
            var pokemon =  _mapper.Map<List<PokemonDto>>(_pokemonRepo.GetPokemons(pageNumber));
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