using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using model.Models;

namespace JamesRecipes.Service.Admin;

public class RecipeManagementService: IRecipeManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public RecipeManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<ViewRecipeManagement> GetAllRecipes()
    {
        return _db.ViewRecipeManagements.ToList();
    }

    public ViewRecipeManagement GetRecipe(int id)
    {
        return _db.ViewRecipeManagements.SingleOrDefault(r => r.RecipeId == id)!;
    }
}