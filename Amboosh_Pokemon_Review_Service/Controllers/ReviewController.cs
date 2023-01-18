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

namespace Amboosh_Pokemon_Review_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepo reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }
        
        // GET: api/Review
        [HttpGet("{pageNumber}/page_number")]
        public IActionResult GetReviews(int pageNumber)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepo.GetReviews(pageNumber));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        // GET: api/Review/5
        [HttpGet("{reviewID}/review")]
        public IActionResult GetReview(int reviewID)
        {
            var review = _mapper.Map<ReviewDto>(_reviewRepo.Review(reviewID));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }
        
        // GET: api/Review/5/pokemon_review
        [HttpGet("{pokemonId}/pokemon_review")]
        public IActionResult GetReviewOfPokemon(int pokemonId)
        {
            var review = _mapper.Map<List<ReviewDto>>(_reviewRepo.GetReviewOfPokemon(pokemonId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        // // POST: api/Review
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }
        //
        // // PUT: api/Review/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
        //
        // // DELETE: api/Review/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}