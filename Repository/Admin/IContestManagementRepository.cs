using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IContestManagementRepository
{
    List<Contest> GetContests();
    Contest GetContest(int id);
    void AddContest(Contest contest);
    void UpdateContest(Contest contest);
    void DeleteContest(int id);
    

    
}