using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.Admin;

public class RecipeCategoriesManagementService: IRecipeCategoriesManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public RecipeCategoriesManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    
    public List<CategoriesRecipe> GetAllCategories()
    {
        return _db.CategoriesRecipes
            .Include(r=>r.Recipes)
            .ToList();
    }

    public CategoriesRecipe GetCategory(int id)
    {
        var rep = _db.CategoriesRecipes.SingleOrDefault(c => c.CategoryRecipeId == id);
        return rep;
    }

    public void PostCategory(CategoriesRecipe newCategoriesRecipe)
    {
        _db.CategoriesRecipes.Add(newCategoriesRecipe);
        _db.SaveChanges();
    }

    public void PutCategory(int id, CategoriesRecipe categoriesRecipe)
    {
        var model = GetCategory(id);
        model!.CategoryName = categoriesRecipe.CategoryName;
        _db.SaveChanges();
    }
    

    public void DeleteCategory(int id)
    {
        var model = _db.CategoriesRecipes.SingleOrDefault(c => c.CategoryRecipeId == id);
        if (model != null)
        {
            _db.CategoriesRecipes.Remove(model);
            _db.SaveChanges();
        }
    }

    public bool CheckRecipe(CategoriesRecipe categoriesRecipe)
    {
        var model = _db.CategoriesRecipes.Include(c => c.Recipes).SingleOrDefault(c => c.CategoryRecipeId == categoriesRecipe.CategoryRecipeId);
        if (model!.Recipes.Any())
        {
            return true;
        }
        return false;
    }

    public List<Recipe> GetRecipeByCategory(int id)
    {
        return _db.Recipes
            .Include(c=>c.CategoryRecipe)
            .Include(c=>c.User)
            .Where(r => r.CategoryRecipeId == id).ToList();
    }
}