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
            var reviewer = _mapper.Map<List<ReviewerDto>>(_reviewerRepo.GetReviewers());
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
            var reviewer = _reviewerRepo.GetReviewer(reviewerId);
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

        // POST: api/Reviewer
        [HttpPost]
        public IActionResult PostReviewer([FromBody] ReviewerDto createReviewer)
        {
            if (createReviewer == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepo.GetReviewers()
                .Where(rr => rr.LastName.Trim().ToUpper() == createReviewer.LastName.Trim().ToUpper()).FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("","Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(createReviewer);

            if (!_reviewerRepo.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("","Something went wrong whiles adding your reviewer");
                return StatusCode(500, ModelState);
            }

            return Ok("Reviewer has been added successfully"); 
        }
        
        // PUT: api/Reviewer/5
        [HttpPut("{reviewerId}")]
        public IActionResult PutReviewer(int reviewerId, [FromBody] ReviewerDto createReviewer)
        {
            if(createReviewer == null)
                return BadRequest(ModelState);

            if (reviewerId != createReviewer.Id)
                return BadRequest(ModelState);

            if (!_reviewerRepo.ReviewerExist(reviewerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewer = _mapper.Map<Reviewer>(createReviewer);

            if (!_reviewerRepo.UpdateReviewer(reviewer))
            {
                ModelState.AddModelError("","Something went wrong whiles updating Reviewer");
                return StatusCode(500, ModelState);
            }

            return Ok("Reviewer has been updated successfully.");


        }
        
        // // DELETE: api/Reviewer/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}