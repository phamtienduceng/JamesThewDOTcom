using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using model.Models;

namespace JamesRecipes.Service.Admin;

public class TipManagementService: ITipManagementRepository
{
    private readonly JamesrecipesContext _db;
    
    public TipManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
}