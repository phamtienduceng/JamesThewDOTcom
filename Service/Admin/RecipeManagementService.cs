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
}