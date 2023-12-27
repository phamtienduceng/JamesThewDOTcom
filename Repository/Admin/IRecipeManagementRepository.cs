using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IRecipeManagementRepository
{
    List<Recipe> GetAllRecipes();

    Recipe GetRecipe(int id);
}