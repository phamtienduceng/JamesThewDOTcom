using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IRecipeCategoriesManagementRepository
{
    List<CategoriesRecipe> GetAllCategories();

    CategoriesRecipe GetCategory(int id);

    void PostCategory(CategoriesRecipe newCategoriesRecipe);

    void PutCategory(int id, CategoriesRecipe categoriesRecipe);

    void DeleteCategory(int id);

    bool CheckRecipe(CategoriesRecipe categoriesRecipe);

    List<Recipe> GetRecipeByCategory(int id);
}