using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amboosh_Pokemon_Review_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepo _ownerRepo;
        private readonly ICountryRepo _countryRepo;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepo ownerRepo, ICountryRepo countryRepo, IMapper mapper)
        {
            _ownerRepo = ownerRepo;
            _mapper = mapper;
            _countryRepo = countryRepo;
        }
        
        // GET: api/Owner
        [HttpGet]
        public IActionResult GetOwners()
        {
            var owner = _mapper.Map<List<OwnerDto>>(_ownerRepo.GetOwners());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        // GET: api/Owner/5
        [HttpGet("{ownerId}")]
        public IActionResult GetOwner (int ownerId)
        {
            var owner = _mapper.Map<OwnerDto>(_ownerRepo.GetOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }
        
        // GET: api/OwnerByPokemon/5
        [HttpGet("{pokemonId}/owner")]
        public IActionResult GetOwnerByPokemon (int pokemonId)
        {
            var owner = _mapper.Map<List<OwnerDto>>(_ownerRepo.GetOwnerOfAPokemon(pokemonId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }
        
        // GET: api/OwnerByPokemon/5
        [HttpGet("{ownerId}/pokemon")]
        public IActionResult GetPokemonByOwner (int ownerId)
        {
            var owner = _mapper.Map<List<PokemonDto>>(_ownerRepo.GetPokemonByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        // POST: api/Owner
        [HttpPost]
        public IActionResult Post([FromQuery] int countryId, [FromBody] OwnerDto createOwnerDto)
        {
            if (createOwnerDto == null)
                return BadRequest(ModelState);

            var owner = _ownerRepo.GetOwners()
                .Where(o => o.LastName.Trim().ToUpper() == createOwnerDto.LastName.Trim().ToUpper()).FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "This owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Owner>(createOwnerDto);
            ownerMap.Country = _countryRepo.GetCountry(countryId);

            if (!_ownerRepo.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("","There was a problem whiles adding your Owner");
                return StatusCode(500, ModelState);
            }
            return Ok("Owner has been added successfully");
        }
        
        // // PUT: api/Owner/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
        //
        // // DELETE: api/Owner/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}