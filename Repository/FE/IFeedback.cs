using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface IFeedback
{
    List<Feedback> GetFeedbacksByRecipeId(int recipeId);
    
    void AddFeedback(Feedback feedback);
}