using JamesRecipes.Models;
using JamesRecipes.Models.Book;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE;

public class FeedbackService: IFeedback
{
    private readonly JamesrecipesContext _db;

    public FeedbackService(JamesrecipesContext db)
    {
        _db = db;
    }
    
    public List<Feedback> GetFeedbacksByRecipeId(int recipeId)
    {
        return _db.Feedbacks
            .Where(f => f.RecipeId == recipeId)
            .ToList();
    }

    public List<Feedback> GetFeedbacksByTipId(int tipId)
    {
        return _db.Feedbacks
            .Where(f => f.TipId == tipId)
            .ToList();
    }

    public void AddFeedback(Feedback feedback)
    {
        _db.Feedbacks.Add(feedback);
        _db.SaveChanges();
    }
}