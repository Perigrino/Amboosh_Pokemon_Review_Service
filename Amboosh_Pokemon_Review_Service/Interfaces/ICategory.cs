using Amboosh_Pokemon_Review_Service.Model;

namespace Amboosh_Pokemon_Review_Service.Interfaces;

public interface ICategory
{
    ICollection<Category> GetCategories();
    Category GetCategory(int categoryId);
    ICollection<Pokemon> GetPokemonsByCategory(int categoryId);
    bool CategoryExists(int id);

    bool CreateCategory(Category Category);
    bool UpdateCategory(Category category);
    bool DeleteCategory(Category category);
    bool Save();
}