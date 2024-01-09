using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IContestManagementRepository
{
    List<Contest> GetContests();
    Contest GetContest(int id);
    void AddContest(Contest contest);
    void UpdateContest(int id, Contest contest);
    void DeleteContest(int id);
    bool CheckContest(Contest contest);
    List<ContestEntry> GetAllContestEntries(int id);
    // isActive = true => get all active contests
    // isActive = false => get all inactive contests
    //List<Contest> GetContests(bool isActive);
}