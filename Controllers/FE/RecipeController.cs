using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using X.PagedList;

namespace JamesRecipes.Controllers.FE;


public class RecipeController : Controller
{
    private readonly IRecipe _recipe;
    private readonly ICategoriesRecipe _categoriesRecipe;
    private readonly IFeedback _feedback;
    private readonly IContest _contest;
    

    public RecipeController(IRecipe recipe, ICategoriesRecipe categoriesRecipe, IFeedback feedback, IContest contest)
    {
        _recipe = recipe;
        _categoriesRecipe = categoriesRecipe;
        _feedback = feedback;
        _contest = contest;
        
    }

    public IActionResult Index(string sortOrder, string searchString, int? categoryId, TimeSpan? timeMin, TimeSpan? timeMax, int? ratingMin, int? ratingMax, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";
        ViewData["CurrentFilter"] = searchString;
        
        var userJson = HttpContext.Session.GetString("userLogged");
        
        var reps = _recipe.GetAllRecipes();
        
        if (!string.IsNullOrEmpty(userJson))
        {
            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user!.RoleId == 1 || user.RoleId == 3)
            {
                reps = _recipe.GetAllRecipesPremium();
            }
            
        }        

        if (!string.IsNullOrEmpty(searchString))
        {
            reps = _recipe.Search(searchString);
        }
        
        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName", categoryId);
        if (categoryId != 0 || timeMin != null || timeMax != null || ratingMin != 0 || ratingMax != 0)
        {
            reps = _recipe.Filter(categoryId, timeMin, timeMax, ratingMin, ratingMax, reps);
        }
        reps = _recipe.Sorting(reps, sortOrder);
        
        page = page < 1 ? 1 : page;
        var recipes = _recipe.PageList(page, 9, reps);

        return View("~/Views/FE/Recipe/Index.cshtml", recipes);
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
        var newFeedback = new Feedback
        {
            RecipeId = recipeId,
            UserId = userId,
            Content = content,
            Rating = rating,
        };
        
        _feedback.AddFeedback(newFeedback);
        _recipe.UpdateRatingRecipe(recipeId);
        
        var updatedComments = _feedback.GetFeedbacksByRecipeId(recipeId).ToList();
        return PartialView("_CommentsPartial", updatedComments);
    }

    [HttpPost("switch_status")]
    public IActionResult SwitchStatus(int id, bool status)
    {
        _recipe.SwitchStatus(id, status);
        return RedirectToAction("GetRecipesByUser");
    }
    
    [HttpPost("switch_premium")]
    public IActionResult SwitchPremium(int id, bool isPre)
    {
        _recipe.PremiumStatus(id, isPre);
        return RedirectToAction("GetRecipesByUser");
    }

    public IActionResult GetRecipesByUser(int id, string sortOrder, string searchString, int? categoryId, TimeSpan? timeMin, TimeSpan? timeMax, int? ratingMin, int? ratingMax, int page = 1)
    {
        ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";
        ViewData["CurrentFilter"] = searchString;
        
        var userJson = HttpContext.Session.GetString("userLogged");
        
        var reps = _recipe.GetRecipesByUser(id);

        if (!string.IsNullOrEmpty(searchString))
        {
            reps = _recipe.Search(searchString);
        }
        
        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName", categoryId);
        if (categoryId != 0 || timeMin != null || timeMax != null || ratingMin != 0 || ratingMax != 0)
        {
            reps = _recipe.Filter(categoryId, timeMin, timeMax, ratingMin, ratingMax, reps);
        }
        reps = _recipe.Sorting(reps, sortOrder);
        
        page = page < 1 ? 1 : page;
        var recipes = _recipe.PageList(page, 9, reps);
        
        return View("~/Views/FE/Recipe/MyRecipe.cshtml", recipes);
    }

    public IActionResult DeleteMyRecipe(int recipeId, int userId)
    {
        _recipe.DeleteMyRecipe(recipeId);
        return RedirectToAction("GetRecipesByUser", new {id = userId});
    }
    
    [HttpGet("update_recipe")]
    public IActionResult UpdateRecipe(int id)
    {
        var rep = _recipe.GetOneRecipe(id);
        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName");
        return View("~/Views/FE/Recipe/Update.cshtml", rep);
    }




    // đoạn mã này của anh Trí để tạo recipe riêng cho user contest entries
    [HttpGet("UserContestRecipeCreatePartial")]
    public IActionResult UserContestRecipeCreatePartial()
    {
        // Lấy UserId từ session hoặc JSON
        string userJson = HttpContext.Session.GetString("userLogged");
        User currentUser;
        int userId = 0;
        if (!string.IsNullOrEmpty(userJson))
        {
            currentUser = JsonConvert.DeserializeObject<User>(userJson);
            userId = currentUser.UserId;
        }

        // Lấy ContestId từ session
        string contestIdSession = HttpContext.Session.GetString("contestId");
        int? contestId = null;
        if (!string.IsNullOrEmpty(contestIdSession))
        {
            contestId = int.Parse(contestIdSession);
        }

        // Lưu UserId và ContestId vào ViewData nếu muốn sử dụng trong View
        ViewData["UserId"] = userId;
        ViewData["ContestId"] = contestId;

        ViewBag.CategoryId = new SelectList(_categoriesRecipe.GetCategoriesRecipes(), "CategoryRecipeId", "CategoryName");
        return View("~/Views/Shared/UserContestRecipeCreatePartial.cshtml");
    }

    [HttpPost("UserContestRecipeCreatePartial")]
    public async Task<IActionResult> UserContestRecipeCreatePartial(Recipe newRecipe, IFormFile file)
    {
        try
        {
            // Lấy UserId và ContestId từ ViewData hoặc trực tiếp từ session
            string userJson = HttpContext.Session.GetString("userLogged");
            User currentUser;
            int userId = 0;
            if (!string.IsNullOrEmpty(userJson))
            {
                currentUser = JsonConvert.DeserializeObject<User>(userJson);
                userId = currentUser.UserId;
            }
            string contestIdSession = HttpContext.Session.GetString("contestId");
            int? contestId = null;
            if (!string.IsNullOrEmpty(contestIdSession))
            {
                contestId = int.Parse(contestIdSession);
            }

            // Xử lý file và lưu Recipe mới
            if (file != null)
            {
                var path = Path.Combine("wwwroot/fe/img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                newRecipe.Image = "fe/img/" + file.FileName;
                newRecipe.UserId = userId; // Đặt UserId cho recipe
                
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
        return View("~/Views/FE/Recipe/UserContestRecipeCreatePartial.cshtml");
    }

    // kết thúc phần mã của anh Trí.
}
