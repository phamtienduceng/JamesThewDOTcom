using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Controllers.FE;

public class FeedbackController : Controller
{
    private readonly IFeedback _feedback;
    private readonly IRecipe _recipe;

    public FeedbackController(IFeedback feedback, IRecipe recipe)
    {
        _feedback = feedback;
        _recipe = recipe;
    }
}