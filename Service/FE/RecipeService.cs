using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

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

    public void PostRecipe(Recipe newRecipe)
    {
        _db.Recipes.Add(newRecipe);
        _db.SaveChanges();
    }
}