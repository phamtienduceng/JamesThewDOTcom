using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface IRecipe
{
    List<Recipe> GetAllRecipes();
    void PostRecipe(Recipe newRecipe);
}