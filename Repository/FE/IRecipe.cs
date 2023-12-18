using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface IRecipe
{
    List<Recipe> GetAllRecipes();

    Recipe GetRecipe(int id);
    void PostRecipe(Recipe newRecipe);
}