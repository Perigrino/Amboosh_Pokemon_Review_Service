using Amboosh_Library.Data.Paging;
using Amboosh_Pokemon_Review_Service.Data;
using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Interfaces;
using Amboosh_Pokemon_Review_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Amboosh_Pokemon_Review_Service.Repository;

public class CategoryRepo : ICategory
{
    private readonly AppDbContext _context;
    public CategoryRepo(AppDbContext context)
    {
        _context = context;
    }
    
    
    public ICollection<Category> GetCategories()
    {
        var category = _context.Categories.OrderBy(c => c.Id).ToList();
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
            .Include(r => r.Pokemon.Reviews)
            .Include(o => o.Pokemon.PokemonOwners)
            .Include(c => c.Pokemon.PokemonCategories)
            .Where(pc => pc.CategoryId == categoryId)
            .Select(c => c.Pokemon)
            .ToList();
        return categoryPokemon;
    }

    public bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

    public bool CreateCategory(Category createCategory)
    {
        var category = _context.Add(createCategory);
        return Save();
    }

    public bool UpdateCategory(Category createCategory)
    {
        var category = _context.Update(createCategory);
        return Save();
    }

    public bool DeleteCategory(Category category)
    {
        _context.Remove(category);
        return Save();
    }

    public bool Save()
    {
        var save = _context.SaveChanges();
        return save > 0 ? true : false;
    }
}