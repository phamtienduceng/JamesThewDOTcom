using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Service.Admin;

public class ContestManagementService : IContestManagementRepository
{
    private readonly JamesrecipesContext _db;
    public ContestManagementService(JamesrecipesContext db)
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
        return contest ?? null;
    }
    public void AddContest(Contest contest)
    {
        _db.Contests.Add(contest);
        _db.SaveChanges();
    }
    public void UpdateContest(int id, Contest updatedContest)
    {
        var existingContest = GetContest(id);
        if (existingContest != null)
        {
            existingContest.Title = updatedContest.Title;
            existingContest.Guidelines = updatedContest.Guidelines;
            existingContest.StartDate = updatedContest.StartDate;
            existingContest.EndDate = updatedContest.EndDate;
            existingContest.IsActive = updatedContest.IsActive;
            existingContest.Image = updatedContest.Image;

            _db.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Contest with the provided id does not exist.", nameof(id));
        }
    }

    public void DeleteContest(int id)
    {
        var contest = _db.Contests.SingleOrDefault(c => c.ContestId == id);
        if (contest != null)
        {
            _db.Contests.Remove(contest);
            _db.SaveChanges();
        }
    }

    public bool CheckContest(Contest contest)
    {
        var model = _db.Contests.Include(c => c.ContestEntries).SingleOrDefault(c => c.ContestId == contest.ContestId);
        if (model!.ContestEntries.Any())
        {
            return true;
        }
        return false;
    }

    public List<ContestEntry> GetAllContestEntries(int id)
    {
        return _db.ContestEntries.Where(c => c.ContestId == id).ToList();
    }
}