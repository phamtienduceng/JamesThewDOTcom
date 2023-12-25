using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.FE;

public interface ITip
{
    List<Tip> GetAllTips();

    List<Tip> GetAllTipsPremium();
    
    Tip GetTip(int id);

    List<Tip> GetTipsByUser(int id);
    
    void PostTip(Tip newTip);

    List<Tip> Sorting(List<Tip> tips, string sortOrder);
    
    List<Tip> Search(string searchString);

    void SwitchStatus(int id, bool status);

    IPagedList<Tip> PageList(int page, int pageSize, List<Tip> tips);

    void UpdateRatingTip(int tipId);

    List<Tip> Filter(int? categoryId, int? ratingMin, int? ratingMax, List<Tip> tips);

    void DeleteMyTip(int id);
}