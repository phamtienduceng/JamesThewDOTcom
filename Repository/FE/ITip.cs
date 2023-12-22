using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface ITip
{
    List<Tip> GetAllTips();
    Tip GetTip(int id);
    void PostTip(Tip newTip);
}