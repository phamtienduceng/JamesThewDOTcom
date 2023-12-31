using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.FE;

public class TipService: ITip
{
    private readonly JamesrecipesContext _db;

    public TipService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<Tip> GetAllTips()
    {
        return _db.Tips.ToList();
    }

    public Tip GetTip(int id)
    {
        var tip = _db.Tips.Include(r=>r.Feedbacks).SingleOrDefault(r => r.TipId == id);
        return tip ?? null!;
    }

    public void PostTip(Tip newTip)
    {
        _db.Tips.Add(newTip);
        _db.SaveChanges();
    }
}