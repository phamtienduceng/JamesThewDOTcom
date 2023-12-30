using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace JamesRecipes.Service.Admin;

public class RecipeManagementService: IRecipeManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public RecipeManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<Recipe> GetAllRecipes()
    {
        return _db.Recipes.Include(f=>f.User)
            .ThenInclude(f=>f!.Role)
            .Include(f=>f.CategoryRecipe)
            .OrderByDescending(f=>f.CreatedAt)
            .ToList();
    }

    public Recipe GetRecipe(int id)
    {
        return _db.Recipes.Include(r=>r.Feedbacks)
            .ThenInclude(f=>f.User)
            .ThenInclude(f=>f.Role)
            .Include(f=>f.CategoryRecipe)
            .SingleOrDefault(r => r.RecipeId == id)!;
    }

    public IPagedList<Recipe> PagedList(int page, int pageSize, List<Recipe> recipes)
    {
        return recipes.ToPagedList(page, pageSize);
    }
}