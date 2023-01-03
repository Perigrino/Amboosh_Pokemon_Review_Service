using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("pagenumber{pageNumber}")]
        public IActionResult GetCountries(int pageNumber)
        {
            var country = _mapper.Map<List<CountryDto>>(_countryRepo.GetCountries(pageNumber));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        // GET: api/Country/5
        [HttpGet("{countryId}/country")]
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
        [HttpGet("{ownerId}/owner")]
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
        [HttpGet("{countryId}/countries")]
        public IActionResult GetOwnersFromCountry(int countryId)
        {
            var country = _countryRepo.GetOwnersFromCountry(countryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }
        
        
        

        // // POST: api/Country
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }
        //
        // // PUT: api/Country/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
        //
        // // DELETE: api/Country/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}