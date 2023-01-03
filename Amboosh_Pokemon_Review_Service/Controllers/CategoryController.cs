using Microsoft.AspNetCore.Mvc;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using AutoMapper;

namespace Amboosh_Pokemon_Review_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategory categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet("pagenumber{pageNumber}")]
        public  IActionResult GetCategories(int pageNumber)
        {
            var category =  _mapper.Map<List<CategoryDto>>(_categoryRepo.GetCategories(pageNumber));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        // GET: api/Category/5
        [HttpGet("{categoryId}")]
        public  IActionResult GetCategory(int categoryId)
        {
            var category =  _mapper.Map<CategoryDto>(_categoryRepo.GetCategory(categoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }
        
        //GET: api/PokemonCategory/5
        [HttpGet("{categoryId}/PokemonCategory")]
        public IActionResult GetPokemonCategory(int categoryId)
        {
            var pokemon = _categoryRepo.GetPokemonsByCategory(categoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }
    }
}