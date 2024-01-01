using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.FE;

public interface IRecipe
{
    List<Recipe> GetAllRecipes();

    List<Recipe> GetAllRecipesPremium();
    
    Recipe GetRecipe(int id);

    Recipe GetOneRecipe(int id);

    List<Recipe> GetRecipesByUser(int id);
    void PostRecipe(Recipe newRecipe);

    List<Recipe> Sorting(List<Recipe> recipes, string sortOrder);
    
    List<Recipe> Search(string searchString);

    void SwitchStatus(int id, bool status);
    
    void PremiumStatus(int id, bool isPre);

    IPagedList<Recipe> PageList(int page, int pageSize, List<Recipe> recipes);

    void UpdateRatingRecipe(int recipeId);

    List<Recipe> Filter(int? categoryId, TimeSpan? timeMin, TimeSpan? timeMax, int? ratingMin, int? ratingMax, List<Recipe> recipes);

    void DeleteMyRecipe(int id);

    void UpdateRecipe(int id, Recipe newRecipe);

    List<Recipe> RelatedRecipes();
}