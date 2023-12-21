using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace JamesRecipes.Service.FE;

public class RecipeService: IRecipe
{
    private readonly JamesrecipesContext _db;

    public RecipeService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Recipe> GetAllRecipes()
    {
        return _db.Recipes.Where(r=>r.Status == true).ToList();
    }

    public List<Recipe> GetAllRecipesPremium()
    {
        return _db.Recipes.Where(r => r.IsMembershipOnly).ToList();
    }

    public Recipe GetRecipe(int id)
    {
        var rep = _db.Recipes.Include(r=>r.Feedbacks).SingleOrDefault(r => r.RecipeId == id);
        return rep ?? null!;
    }

    public List<Recipe> GetRecipesByUser(int id)
    {
        return _db.Recipes.Where(r => r.UserId == id).ToList();
    }

    public void PostRecipe(Recipe newRecipe)
    {
        _db.Recipes.Add(newRecipe);
        _db.SaveChanges();
    }

    public List<Recipe> Sorting(List<Recipe> recipes, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                recipes = recipes.OrderByDescending(r => r.Title).ToList();
                break;
            case "name_asc":
                recipes = recipes.OrderBy(r => r.Title).ToList();
                break;
            case "Date":
                recipes = recipes.OrderBy(r => r.CreatedAt).ToList();
                break;
            case "date_desc":
                recipes = recipes.OrderByDescending(r => r.CreatedAt).ToList();
                break;
            default:
                recipes = recipes.OrderBy(r => r.CreatedAt).ToList();
                break;
        }
        return recipes;
    }

    public List<Recipe> Search(string searchString)
    {
        return _db.Recipes.Where(r => r.Title.Contains(searchString)).ToList();
    }

    public void SwitchStatus(int id, bool status)
    {
        var rep = _db.Recipes.SingleOrDefault(r => r.RecipeId == id);
        if (rep != null)
        {
            rep.Status = status;
            _db.SaveChanges(); 
        }
    }

    public IPagedList<Recipe> PageList(int page, int pageSize,  List<Recipe> recipes)
    {
        return recipes.ToPagedList(page, pageSize);
    }
}