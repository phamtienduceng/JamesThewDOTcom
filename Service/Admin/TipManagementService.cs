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
    public List<ViewTipManagement> GetAllTips()
    {
        return _db.ViewTipManagements.ToList();
    }

    public ViewTipManagement GetTip(int id)
    {
        return _db.ViewTipManagements.SingleOrDefault(t => t.TipId == id)!;
    }
}