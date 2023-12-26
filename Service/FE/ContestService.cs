using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;
using X.PagedList;



namespace JamesRecipes.Service.FE;

public class ContestService: IContest
{
    private readonly JamesrecipesContext _db;
    public ContestService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Contest> GetContests()
    {
        return _db.Contests.ToList();
    }
    public Contest GetContest(int id)
    {
        var contest = _db.Contests.FirstOrDefault(c => c.ContestId == id);
        return contest ?? null!;
    }
    public List<Contest> Sorting(List<Contest> contests, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                contests = contests.OrderByDescending(c => c.Title).ToList();
                break;
            case "name_asc":
                contests = contests.OrderBy(c => c.Title).ToList();
                break;
            case "Date":
                contests = contests.OrderBy(c => c.StartDate).ToList();
                break;
            case "date_desc":
                contests = contests.OrderByDescending(c => c.StartDate).ToList();
                break;
            default:
                contests = contests.OrderBy(c => c.StartDate).ToList();
                break;
        }
        return contests;
    }
    public List<Contest> Search(string searchString)
    {
        return _db.Contests.Where(c => c.Title.Contains(searchString)).ToList();
    }
    public IPagedList<Contest> PageList(int page, int pageSize, List<Contest> contests)
    {
        return contests.ToPagedList(page, pageSize);
    }

    public List<Contest> Filter(DateTime? startDate, DateTime? endDate, List<Contest> contests)
    {
        if (startDate != null && endDate != null)
        {
            contests = contests.Where(c => c.StartDate >= startDate && c.EndDate <= endDate).ToList();
        }
        else if (startDate != null)
        {
            contests = contests.Where(c => c.StartDate >= startDate).ToList();
        }
        else if (endDate != null)
        {
            contests = contests.Where(c => c.EndDate <= endDate).ToList();
        }
        return contests;
    }
}