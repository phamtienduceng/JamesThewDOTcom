using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.Admin;

public interface IRecipeManagementRepository
{
    List<Recipe> GetAllRecipes();

    Recipe GetRecipe(int id);

    IPagedList<Recipe> PagedList(int page, int pageSize, List<Recipe> recipes);
}