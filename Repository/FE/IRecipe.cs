using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.FE;

public interface IRecipe
{
    List<Recipe> GetAllRecipes();

    List<Recipe> GetAllRecipesPremium();
    
    Recipe GetRecipe(int id);

    List<Recipe> GetRecipesByUser(int id);
    void PostRecipe(Recipe newRecipe);

    List<Recipe> Sorting(List<Recipe> recipes, string sortOrder);
    
    List<Recipe> Search(string searchString);

    void SwitchStatus(int id, bool status);

    IPagedList<Recipe> PageList(int page, int pageSize, List<Recipe> recipes);
}