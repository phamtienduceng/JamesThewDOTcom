using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;

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
    public void UpdateContest(Contest contest)
    {
        _db.Contests.Update(contest);
        _db.SaveChanges();

    }
    public void DeleteContest(int id)
    {
        _db.Contests.Remove(GetContest(id));
        _db.SaveChanges();

    }


}