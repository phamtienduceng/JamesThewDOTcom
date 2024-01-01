using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface ITipManagementRepository
{
    List<Tip> GetAllTips();
    
    Tip GetTip(int id);
}