using Amboosh_Library.Data.Paging;
using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Repository;

public class CategoryRepo : ICategory
{
    private readonly AppDbContext _context;
    public CategoryRepo(AppDbContext context)
    {
        _context = context;
    }
    
    
    public ICollection<Category> GetCategories(int? pageNumber)
    {
        var category = _context.Categories.OrderBy(c => c.Id).ToList();
        
        //Paging
        int pageSize = 10;
        category = PaginatedList<Category>.Create(category.AsQueryable(), pageNumber ?? 1, pageSize);
        return category;
    }

    public Category GetCategory(int categoryId)
    {
        var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        return category;
    }

    public ICollection<Pokemon> GetPokemonsByCategory(int categoryId)
    {
        var categoryPokemon = _context.PokemonCategories
            .Where(pc => pc.CategoryId == categoryId)
             .Select(c => c.Pokemon).ToList();
        return categoryPokemon;
    }

    public bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }
}