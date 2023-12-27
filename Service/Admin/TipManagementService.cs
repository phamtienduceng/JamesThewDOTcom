using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.Admin;

public class TipManagementService: ITipManagementRepository
{
    private readonly JamesrecipesContext _db;
    
    public TipManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<Tip> GetAllTips()
    {
        return _db.Tips.Include(f=>f.User)
            .ThenInclude(f=>f!.Role)
            .Include(f=>f.CategoryTip).ToList();
    }

    public Tip GetTip(int id)
    {
        return _db.Tips.Include(f => f.Feedbacks)
            .Include(f => f.User)
            .ThenInclude(f => f.Role)
            .Include(f => f.CategoryTip)
            .SingleOrDefault(f => f.TipId == id);   
    }
}