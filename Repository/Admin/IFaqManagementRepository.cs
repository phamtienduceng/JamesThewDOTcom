using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.Admin;

public interface IFaqManagementRepository
{
    List<Faq> GetAll();

    Faq GetOneFaq(int id);

    Faq CreateFaq(Faq newFaq);

    Faq UpdateFaq(int id, Faq faq);

    void DeleteFaq(int id);
    
    IPagedList<Faq> PagedList(int page, int pageSize, List<Faq> faq);
    
    List<Faq> Sorting(List<Faq> recipes, string sortOrder);
    
    List<Faq> Search(string searchString);
}