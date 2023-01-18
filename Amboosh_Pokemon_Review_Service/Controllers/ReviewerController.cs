using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amboosh_Pokemon_Review_Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amboosh_Pokemon_Review_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepo _reviewerRepo;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerRepo reviewerRepo, IMapper mapper)
        {
            _reviewerRepo = reviewerRepo;
            _mapper = mapper;
        }
        
        // GET: api/Reviewer
        [HttpGet]
        public IActionResult GetReviewers()
        {
            var reviewer = _reviewerRepo.GetReviewers();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }

        // GET: api/Reviewer/5
        [HttpGet("{reviewerId}")]
        public IActionResult GetReviewer(int reviewerId)
        {
            var reviewer = _reviewerRepo.Reviewer(reviewerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }
        
        // GET: api/Reviewer/5
        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var reviewer = _reviewerRepo.GetReviewsByReviewer(reviewerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }

        // // POST: api/Reviewer
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }
        //
        // // PUT: api/Reviewer/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
        //
        // // DELETE: api/Reviewer/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}