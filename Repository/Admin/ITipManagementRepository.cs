using JamesRecipes.Models;
using X.PagedList;


namespace JamesRecipes.Repository.Admin;

public interface ITipManagementRepository
{
    List<Tip> GetAllTip();

    Tip GetTip(int id);

    IPagedList<Tip> PagedList(int page, int pageSize, List<Tip> tips);
    
    
    List<Tip> Sorting(List<Tip> tips, string sortOrder);
    
    List<Tip> Search(string searchString);
    
    byte[] GeneratedExcel(string filename, List<Tip> tips);
}