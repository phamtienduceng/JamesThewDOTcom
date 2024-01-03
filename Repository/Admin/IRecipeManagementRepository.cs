using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.Admin;

public interface IRecipeManagementRepository
{
    List<Recipe> GetAllRecipes();

    Recipe GetRecipe(int id);

    IPagedList<Recipe> PagedList(int page, int pageSize, List<Recipe> recipes);
    
    void SwitchStatus(int id, bool status);
    
    void PremiumStatus(int id, bool isPre);
    
    List<Recipe> Sorting(List<Recipe> recipes, string sortOrder);
    
    List<Recipe> Search(string searchString);
}