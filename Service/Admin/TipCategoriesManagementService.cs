using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.Admin;

public class TipCategoriesManagementService: ITipCategoriesManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public TipCategoriesManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<CategoriesTip> GetAllCategories()
    {
        return _db.CategoriesTips.ToList();
    }

    public CategoriesTip GetCategory(int id)
    {
        var rep = _db.CategoriesTips.SingleOrDefault(c => c.CategoryTipId == id);
        return rep!;
    }

    public void PostCategory(CategoriesTip newCategoriesTip)
    {
        _db.CategoriesTips.Add(newCategoriesTip);
        _db.SaveChanges();
    }

    public void PutCategory(int id, CategoriesTip categoriesTip)
    {
        var model = GetCategory(id);
        model!.CategoryName = categoriesTip.CategoryName;
        _db.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var model = _db.CategoriesTips.SingleOrDefault(c => c.CategoryTipId == id);
        if (model != null)
        {
            _db.CategoriesTips.Remove(model);
            _db.SaveChanges();
        }
    }

    public bool CheckTip(CategoriesTip categoriesTip)
    {
        var model = _db.CategoriesTips.Include(c => c.Tips).SingleOrDefault(c => c.CategoryTipId == categoriesTip.CategoryTipId);
        if (model!.Tips.Any())
        {
            return true;
        }
        return false;
    }

    public List<Tip> GetTipsByCategory(int id)
    {
        return _db.Tips.Where(t => t.CategoryTipId == id).ToList();
    }
}