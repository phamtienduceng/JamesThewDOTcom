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
}