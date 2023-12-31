using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE;

public class HomeService: IHome
{
    private readonly JamesrecipesContext _db;

    public HomeService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<ViewHomepage> GetHomepages()
    {
        return _db.ViewHomepages.ToList();
    }
}