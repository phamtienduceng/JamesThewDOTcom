using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;

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
            .Include(f=>f.CategoryRecipe).ToList();
    }

    public Recipe GetRecipe(int id)
    {
        return _db.Recipes.Include(r=>r.Feedbacks)
            .ThenInclude(f=>f.User)
            .ThenInclude(f=>f.Role)
            .Include(f=>f.CategoryRecipe)
            .SingleOrDefault(r => r.RecipeId == id)!;
    }
}