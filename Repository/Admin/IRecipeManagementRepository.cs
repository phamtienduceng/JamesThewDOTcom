using JamesRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace JamesRecipes.Repository.Admin;

public interface IRecipeManagementRepository
{
    List<Recipe> GetAllRecipes();

    Recipe GetRecipe(int id);

    IPagedList<Recipe> PagedList(int page, int pageSize, List<Recipe> recipes);
    
    List<Recipe> Sorting(List<Recipe> recipes, string sortOrder);
    
    List<Recipe> Search(string searchString);

    byte[] GeneratedExcel(string filename, List<Recipe> recipes);
}