using JamesRecipes.Models.Authentication;
using JamesRecipes.Repository.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.Admin;

[Route("admin/[controller]")]
public class RecipeManagementController : Controller
{
    private readonly IRecipeManagementRepository _recipeManagement;

    public RecipeManagementController(IRecipeManagementRepository recipeManagement)
    {
        _recipeManagement = recipeManagement;
    }
}