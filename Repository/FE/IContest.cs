using JamesRecipes.Models;
using X.PagedList;

namespace JamesRecipes.Repository.FE;

public interface IContest
{
    List<Contest> GetContests();
    Contest GetContest(int id);
    
    List<Contest> Sorting(List<Contest> contests, string sortOrder);
    List<Contest> Search(string searchString);
    IPagedList<Contest> PageList(int page, int pageSize, List<Contest> contests);
    List<Contest> Filter(DateTime? startDate, DateTime? endDate, List<Contest> contests);

}