using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE;

public class CategoriesTipService: ICategoriesTip
{
    private readonly JamesrecipesContext _db;

    public CategoriesTipService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<CategoriesTip> GetCategoriesTips()
    {
       return _db.CategoriesTips.ToList();
    }
}