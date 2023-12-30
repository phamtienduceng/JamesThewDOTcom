using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin
{
    public interface IContestEntryManagementRepository
    {
        List<ContestEntry> GetAllContestEntries();
        ContestEntry GetContestEntry(int id);

      
    }
}
