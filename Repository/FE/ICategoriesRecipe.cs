using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface ICategoriesRecipe
{
    List<CategoriesRecipe> GetCategoriesRecipes();
}