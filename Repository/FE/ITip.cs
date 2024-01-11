using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.FE;

public interface ITip
{
    List<Tip> GetAllTips();

    List<Tip> GetAllTipsPremium();
    
    Tip GetTip(int id);

    Tip GetOneTip(int id);

    List<Tip> GetTipsByUser(int id);
    
    void PostTip(Tip newTip);

    List<Tip> Sorting(List<Tip> tips, string sortOrder);
    
    List<Tip> Search(string searchString, List<Tip> tips);

    void SwitchStatus(int id, bool status);
    
    void PremiumStatus(int id, bool isPre);

    IPagedList<Tip> PageList(int page, int pageSize, List<Tip> tips);

    void UpdateRatingTip(int tipId);

    List<Tip> Filter(int? categoryId, int? ratingMin, int? ratingMax, List<Tip> tips);

    void DeleteMyTip(int id);
    
    void UpdateTip(int id, Tip newTip);

    List<Tip> RelatedTips();
    
    byte[] GeneratedWord(Tip tip);
}