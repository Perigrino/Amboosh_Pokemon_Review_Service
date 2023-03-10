using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Amboosh_Pokemon_Review_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepo _countryRepo;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepo countryRepo, IMapper mapper)
        {
            _countryRepo = countryRepo;
            _mapper = mapper;
        }
        
        
        // GET: api/Country
        [HttpGet]
        public IActionResult GetCountries()
        {
            var country = _mapper.Map<List<CountryDto>>(_countryRepo.GetCountries());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        // GET: api/Country/5
        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {
            var country = _mapper.Map<CountryDto>(_countryRepo.GetCountry(countryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }
        
        // GET: api/CountryByOwner/5
        [HttpGet("ownerId/{ownerId}")]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(_countryRepo.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }
        
        
        // GET: api/CountryByOwner/5
        [HttpGet("find_owner_with_{countryId}")]
        public IActionResult GetOwnersFromCountry(int countryId)
        {
            var country = _countryRepo.GetOwnersFromCountry(countryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }
        
        
        // POST: api/Country
        [HttpPost]
        public IActionResult PostCountry([FromBody] CountryDto createCountry)
        {
            if (createCountry == null)
                return BadRequest(ModelState);

            var country = _countryRepo.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == createCountry.Name.Trim().ToUpper()).FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("","Country already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(createCountry);

            if (!_countryRepo.CreateCountry(countryMap))
            {
                ModelState.AddModelError("","Something went wrong whiles adding your Country");
                return StatusCode(500, ModelState);
            }

            return Ok("Country has been created successfully");
        }
        
        // PUT: api/Country/5
        [HttpPut("{countryId}")]
        public IActionResult Put(int countryId, [FromBody] CountryDto country)
        {
            if (country == null)
                return BadRequest(ModelState);

            if (countryId != country.Id)
                return BadRequest(ModelState);

            if (!_countryRepo.CountryExists(countryId))
                return NotFound(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(country);

            if (!_countryRepo.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong whiles updating the Country");
            }

            return Ok("Country has been updated successfully");
        }
        
        // DELETE: api/Country/5
        [HttpDelete("{countryId}")]
        public IActionResult Delete(int countryId)
        {
            if (!_countryRepo.CountryExists(countryId))
            {
                return NotFound();
            }

            var countryToDelete = _countryRepo.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_countryRepo.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return Ok("Country deleted successfully");
        }
    }
}