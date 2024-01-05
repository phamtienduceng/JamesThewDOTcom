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
            .ThenInclude(r=>r.User)
            .Include(f=>f.User).Include(r=>r.User!.Role)
            .Include(f=>f.CategoryRecipe)
            .SingleOrDefault(r => r.RecipeId == id)!;
    }

    public IPagedList<Recipe> PagedList(int page, int pageSize, List<Recipe> recipes)
    {
        return recipes.ToPagedList(page, pageSize);
    }

    public List<Recipe> Sorting(List<Recipe> recipes, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                recipes = recipes.OrderByDescending(r => r.Title).ToList();
                break;
            case "name_asc":
                recipes = recipes.OrderBy(r => r.Title).ToList();
                break;
            case "Date":
                recipes = recipes.OrderBy(r => r.CreatedAt).ToList();
                break;
            case "date_desc":
                recipes = recipes.OrderByDescending(r => r.CreatedAt).ToList();
                break;
            case "rating":
                recipes = recipes.OrderBy(r => r.Rating).ToList();
                break;
            case "rating_desc":
                recipes = recipes.OrderByDescending(r => r.Rating).ToList();
                break;
            default:
                recipes = recipes.OrderByDescending(r => r.CreatedAt).ToList();
                break;
        }
        return recipes;
    }

    public List<Recipe> Search(string searchString)
    {
        return _db.Recipes.Where(r => r.Title.Contains(searchString)).ToList();
    }
}