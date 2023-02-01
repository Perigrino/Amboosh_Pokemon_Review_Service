using Microsoft.AspNetCore.Mvc;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        [HttpGet]
        public  IActionResult GetCategories()
        {
            var category =  _mapper.Map<List<CategoryDto>>(_categoryRepo.GetCategories());
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
        
        
        // POST: api/Pokemon
        [HttpPost]
        public IActionResult PostCategory([FromBody]  CategoryDto createCategory)
        {
            if (createCategory == null)
                return BadRequest(ModelState);

            var category = _categoryRepo.GetCategories().Where(c => c.Name.Trim().ToUpper() == createCategory.Name.Trim().ToUpper()).FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("","This category already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var catergoryMap = _mapper.Map<Category>(createCategory);

            if (!_categoryRepo.CreateCategory(catergoryMap))
            {
                ModelState.AddModelError("","Something went wrong whiles creating your Category");
                return StatusCode(500, ModelState);
            }

            return Ok("Category has been created successfully");
        }
        
        // PUT: api/Category/5
        [HttpPut("{categoryId}")]
        public IActionResult Put(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest();
            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);
            if (!_categoryRepo.CategoryExists(categoryId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var catergoryMap = _mapper.Map<Category>(updatedCategory);
            
            if (!_categoryRepo.UpdateCategory(catergoryMap))
            {
                ModelState.AddModelError("", "Something went wrong whiles updating this Category");
                return StatusCode(500, ModelState);
            }

            return Ok("Category has been updated successfully");
        }
        
        // // DELETE: api/Country/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}