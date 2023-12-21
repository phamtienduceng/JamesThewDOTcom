using model.Models;

namespace JamesRecipes.Repository.Admin;

public interface IRecipeManagementRepository
{
    List<ViewRecipeManagement> GetAllRecipes();

    ViewRecipeManagement GetRecipe(int id);
}