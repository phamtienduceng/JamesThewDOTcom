using model.Models;

namespace JamesRecipes.Repository.Admin;

public interface ITipManagementRepository
{
    List<ViewTipManagement> GetAllTips();
    
    ViewTipManagement GetTip(int id);
}