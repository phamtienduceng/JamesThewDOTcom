using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.FE;

public class RecipeService: IRecipe
{
    private readonly JamesrecipesContext _db;

    public RecipeService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Recipe> GetAllRecipes()
    {
        return _db.Recipes.ToList();
    }

    public Recipe GetRecipe(int id)
    {
        var rep = _db.Recipes.Include(r=>r.Feedbacks).SingleOrDefault(r => r.RecipeId == id);
        return rep ?? null!;
    }

    public void PostRecipe(Recipe newRecipe)
    {
        _db.Recipes.Add(newRecipe);
        _db.SaveChanges();
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
            default:
                recipes = recipes.OrderBy(r => r.CreatedAt).ToList();
                break;
        }
        return recipes;
    }

    public List<Recipe> Search(string searchString)
    {
        return _db.Recipes.Where(r => r.Title.Contains(searchString)).ToList();
    }
}