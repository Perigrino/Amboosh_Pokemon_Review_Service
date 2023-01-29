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
        private readonly IReviewerRepo _reviewerRepo;
        private readonly IPokemonRepo _pokemonRepo;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepo reviewRepo, IMapper mapper, IPokemonRepo pokemonRepo, IReviewerRepo reviewerRepo)
        {
            _reviewRepo = reviewRepo;
            _pokemonRepo = pokemonRepo;
            _reviewerRepo = reviewerRepo;
            _mapper = mapper;
        }
        
        // GET: api/Review
        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepo.GetReviews());
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

        // POST: api/Review
        [HttpPost]
        public IActionResult PostReview([FromQuery] int reviewerId, [FromQuery] int pokemonId,[FromBody] ReviewDto createReview)
        {
            if (createReview == null)
                return BadRequest(ModelState);

            var review = _reviewRepo.GetReviews().Where(r => r.Title.Trim().ToUpper() == createReview.Title.Trim().ToUpper())
                .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("","Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(createReview);
            reviewMap.Pokemon = _pokemonRepo.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewerRepo.GetReviewer(reviewerId);
 
            if (!_reviewRepo.CreateReview(reviewMap))
            {
                ModelState.AddModelError("","There was a problem when adding your Review");
                return StatusCode(500, ModelState);
            }

            return Ok("Review has been posted successfully");
        }
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