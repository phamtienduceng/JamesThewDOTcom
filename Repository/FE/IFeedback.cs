using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface IFeedback
{
    List<Feedback> GetFeedbacksByRecipeId(int recipeId);
    
    List<Feedback> GetFeedbacksByTipId(int tipId);
    
    void AddFeedback(Feedback feedback);
}