using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE;

public class CategoriesRecipeService: ICategoriesRecipe
{
    private readonly JamesrecipesContext _db;

    public CategoriesRecipeService(JamesrecipesContext db)
    {
        _db = db;
    }
    
    public List<CategoriesRecipe> GetCategoriesRecipes()
    {
        return _db.CategoriesRecipes.ToList();
    }
}