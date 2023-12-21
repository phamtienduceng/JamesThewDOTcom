using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JamesRecipes.Controllers.FE;

[Route("fe/[controller]")]
public class RecipeController : Controller
{
    private readonly IRecipe _recipe;
    private readonly ICategoriesRecipe _categoriesRecipe;
    private readonly IFeedback _feedback;

    public RecipeController(IRecipe recipe, ICategoriesRecipe categoriesRecipe, IFeedback feedback)
    {
        _recipe = recipe;
        _categoriesRecipe = categoriesRecipe;
        _feedback = feedback;
    }

    [HttpGet("index")]
    public IActionResult Index(string sortOrder, string searchString)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";

        if (!string.IsNullOrEmpty(searchString))
        {
            TempData["SearchString"] = searchString;
        }
        else
        {
            searchString = TempData["SearchString"] as string;
        }

        ViewData["CurrentFilter"] = searchString;

        var reps = _recipe.GetAllRecipes();

        if (!string.IsNullOrEmpty(searchString))
        {
            reps = _recipe.Search(searchString);
        }

        reps = _recipe.Sorting(reps, sortOrder);

        return View("~/Views/FE/Recipe/Index.cshtml", reps);
    }

    
    [HttpGet("single_recipe")]
    public IActionResult SingleRecipe(int id)
    {
        var rep = _recipe.GetRecipe(id);
        rep.Feedbacks = _feedback.GetFeedbacksByRecipeId(id);
        return View("~/Views/FE/Recipe/SingleRecipe.cshtml", rep);
    }
    
    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName");
        return View("~/Views/FE/Recipe/Create.cshtml");
    }

    [HttpPost("create")]
    public IActionResult Create(Recipe newRecipe, IFormFile file)
    {
        try
        {
            if (file != null)
            {
                var path = Path.Combine("wwwroot/fe/img", file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                file.CopyToAsync(stream);
                newRecipe.Image = "fe/img/" + file.FileName;
                _recipe.PostRecipe(newRecipe);
                return RedirectToAction("Index");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName");
        return View("~/Views/FE/Recipe/Create.cshtml");
    }
    
    [HttpPost("post_comment")]
    public IActionResult PostFeedback(int recipeId, int userId, string content, int rating)
    {
        // Validate or perform necessary checks

        var newFeedback = new Feedback
        {
            RecipeId = recipeId,
            UserId = userId,
            Content = content,
            Rating = rating,
        };

        _feedback.AddFeedback(newFeedback);
        
        var updatedComments = _feedback.GetFeedbacksByRecipeId(recipeId).ToList();
        return PartialView("_CommentsPartial", updatedComments);
    }

}
